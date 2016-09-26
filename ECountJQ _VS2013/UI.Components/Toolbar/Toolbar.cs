/*
Copyright (c) 2009 Bill Davidsen (wdavidsen@yahoo.com)

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in
all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
THE SOFTWARE.
*/
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Security.Permissions;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace SCS.Web.UI.WebControls
{
    #region Enums
    public enum SelectionModeType
    {
        Off = 0,
        Single,
        Multiple
    }
    #endregion

    #region Class Attributes
    [AspNetHostingPermission(SecurityAction.Demand, Level = AspNetHostingPermissionLevel.Minimal)]
    [AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
    [DefaultEvent("ButtonClicked")]
    [ToolboxData("<{0}:Toolbar runat=\"server\"> </{0}:Toolbar>")]
    [ParseChildren(true, "Items")]
    [Designer(typeof(SCS.Web.UI.WebControls.Design.ToolbarDesigner))]
    #endregion

    public class Toolbar : CompositeControl, IPostBackDataHandler
    {
        #region Fields
        private ToolbarItemCollection _items = null;
        private ButtonClasses _buttonStyle = null;

        private string _clientChanges = "";

        private static string _createParamsJson = @"{{""id"": ""{0}"", ""selectionMode"": ""{1}"", 
            ""buttonCssClass"": ""{2}"", ""buttonCssClassEnabled"": ""{3}"", 
            ""buttonCssClassDisabled"": ""{4}"", ""buttonCssClassSelected"": ""{5}"", 
            ""itemData"": [{6}] }}";

        private static string _eventsJson = @"{{ ""buttonClicked"": {0} }}";
        #endregion

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
        }
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            Page.RegisterRequiresControlState(this);
        }
        protected override void CreateChildControls()
        {
            Controls.Clear();

            if (_items != null)
            {
                for (int i = 0; i < _items.Count; i++)
                {
                    Controls.Add(_items[i].BaseLinkButton);

                    _items[i].Index = i;
                    _items[i].BaseLinkButton.Command += new CommandEventHandler(BaseLinkButton_Command);
                    _items[i].BaseLinkButton.CommandName = _items[i].CommandName;
                    _items[i].BaseLinkButton.CommandArgument = _items[i].CommandArgument;
                }
            }
        }
        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);

            if (Controls.Count != Items.Count)
                CreateChildControls();

            Control parentControl = this.Parent;
            bool inUpdatePanel = false;
            while (parentControl != null)
            {
                if (parentControl is UpdatePanel)
                {
                    inUpdatePanel = true;
                    break;
                }
                else
                {
                    parentControl = parentControl.Parent;
                }
            }
            if (!(inUpdatePanel && Page.IsPostBack))
            {

                SetupClientApi();
            }
        }
        protected override void Render(HtmlTextWriter writer)
        {
            WriteToolbar(writer);
        }

        public override ControlCollection Controls
        {
            get
            {
                EnsureChildControls();
                return base.Controls;
            }
        }

        protected override void LoadControlState(object savedState)
        {
            Dictionary<string, object> dictState = savedState as Dictionary<string, object>;
            if (dictState != null)
            {
                base.LoadControlState(dictState["state"]);
                EnableClientApi = bool.Parse(dictState["EnableClientApi"].ToString());
                BehaviorId = dictState["BehaviorId"].ToString();
                OnClientButtonClick = dictState["OnClientButtonClick"].ToString();
                SelectionMode = (SelectionModeType)dictState["SelectionMode"];
                LastSelectedIndex = Convert.ToInt32(dictState["LastSelectedIndex"]);
            }
        }

        protected override object SaveControlState()
        {
            object state = base.SaveControlState();
            Dictionary<string, object> dictState = new Dictionary<string, object>();
            dictState.Add("state", state);
            dictState.Add("EnableClientApi", EnableClientApi);
            dictState.Add("BehaviorId", BehaviorId);
            dictState.Add("OnClientButtonClick", OnClientButtonClick);
            dictState.Add("SelectionMode", SelectionMode);
            dictState.Add("LastSelectedIndex", LastSelectedIndex);
            return dictState;
        }

        internal virtual void WriteToolbar(HtmlTextWriter writer)
        {
            if (DesignMode && _items == null)
                return;

            AddAttributesToRender(writer);

            writer.AddAttribute(HtmlTextWriterAttribute.Class, this.CssClass);
            writer.RenderBeginTag(HtmlTextWriterTag.Div);

            if (_items != null)
            {
                for (int i = 0; i < _items.Count; i++)
                {
                    _items[i].RenderButton(writer, i);
                }
            }

            writer.AddAttribute(HtmlTextWriterAttribute.Id, ClientID + "_state");
            writer.AddAttribute(HtmlTextWriterAttribute.Name, UniqueID);
            writer.AddAttribute(HtmlTextWriterAttribute.Type, "hidden");
            writer.RenderBeginTag(HtmlTextWriterTag.Input);
            writer.RenderEndTag();

            writer.RenderEndTag();
        }

        #region Event Stuff

        private static readonly object _eventSubmitKey = new object();
        private static readonly object _eventClientChangeKey = new object();

        private void BaseLinkButton_Command(object sender, CommandEventArgs e)
        {
            OnCommand(sender, e);
        }

        [Category("Action"), Description("Raised when the user clicks a button.")]
        public event ButtonClickedHandler ButtonClicked
        {
            add
            {
                Events.AddHandler(_eventSubmitKey, value);
            }
            remove
            {
                Events.RemoveHandler(_eventSubmitKey, value);
            }
        }

        [Category("Action"), Description("Raised when the toolbar is changed through the client API.")]
        public event ClientChangeHandler ClientChange
        {
            add
            {
                Events.AddHandler(_eventClientChangeKey, value);
            }
            remove
            {
                Events.RemoveHandler(_eventClientChangeKey, value);
            }
        }

        protected virtual void OnCommand(object sender, CommandEventArgs e)
        {
            ButtonClickedHandler submitHandler = (ButtonClickedHandler)Events[_eventSubmitKey];

            if (SelectionMode != SelectionModeType.Multiple)
                foreach (ToolbarButton button in Items)
                    button.Selected = false;

            if (SelectionMode != SelectionModeType.Off)
            {
                int currentIndex = Controls.IndexOf((Control)sender);

                if (SelectionMode == SelectionModeType.Single)
                {
                    Items[currentIndex].Selected = true;
                }
                else if (SelectionMode == SelectionModeType.Multiple)
                {
                    // toggle selection
                    Items[currentIndex].Selected = !Items[currentIndex].Selected;
                }

                if (Items[currentIndex].Selected)
                    LastSelectedIndex = currentIndex;
            }
            if (submitHandler != null)
            {
                submitHandler(this, new ButtonEventArgs(e, _items[LastSelectedIndex]));
            }
        }
        protected virtual void OnClientChange(EventArgs e)
        {
            ClientChangeHandler handler = (ClientChangeHandler)Events[_eventClientChangeKey];

            if (handler != null)
            {
                ClientChangeEventArgs args = new ClientChangeEventArgs();

                handler(this, args);

                if (!args.Cancel)
                    return;
            }
        }

        #endregion

        /// <summary>
        /// Setups the client API.
        /// </summary>
        protected virtual void SetupClientApi()
        {
            if (EnableClientApi && Items.Count > 0)
            {
                if (!Util.IsAjaxInstalled)
                {
                    throw new ApplicationException(@"The Ajax assembly System.Web.Extensions 
                        is required by the client API but was not found.");
                }

                ScriptManager manager = Util.GetScriptManager(this.Page);

                ScriptReference sr = new ScriptReference();

                if (HttpContext.Current.IsDebuggingEnabled)
                    manager.Scripts.Add(new ScriptReference("SCS.Web.UI.WebControls.Toolbar.debug.js", "SCS.Web.UI.WebControls.Toolbar"));
                else
                    manager.Scripts.Add(new ScriptReference("SCS.Web.UI.WebControls.Toolbar.js", "SCS.Web.UI.WebControls.Toolbar"));

                StringBuilder buttonData = new StringBuilder(256);

                for (int i = 0; i < _items.Count; i++)
                {
                    ToolbarButton button = _items[i];

                    buttonData.Append("{");

                    if (!string.IsNullOrEmpty(button.ConfirmationMessage))
                        buttonData.AppendFormat(@"""confirmMessage"": ""{0}"", ", button.ConfirmationMessage);

                    if (!string.IsNullOrEmpty(button.CssClassEnabled))
                        buttonData.AppendFormat(@"""enabledCssClass"": ""{0}"", ", button.CssClassEnabled);

                    if (!string.IsNullOrEmpty(button.CssClassDisabled))
                        buttonData.AppendFormat(@"""disabledCssClass"": ""{0}"", ", button.CssClassDisabled);

                    if (!string.IsNullOrEmpty(button.CssClassSelected))
                        buttonData.AppendFormat(@"""selectedCssClass"": ""{0}"", ", button.CssClassSelected);

                    if (button.Enabled && !string.IsNullOrEmpty(button.DisabledImageUrl))
                        buttonData.AppendFormat(@"""disabledImageUrl"": ""{0}"", ", Page.ResolveClientUrl(button.DisabledImageUrl));
                    else
                        buttonData.AppendFormat(@"""imageUrl"": ""{0}"", ", Page.ResolveClientUrl(button.ImageUrl));

                    buttonData.AppendFormat(@"""enabled"": {0}", button.Enabled.ToString().ToLower());
                    buttonData.Append("}, ");
                }

                if (buttonData.Length > 0)
                    buttonData.Remove(buttonData.Length - 2, 2); // remove last comma and space

                string props = string.Format(_createParamsJson,
                    BehaviorId,
                    Enum.GetName(typeof(SelectionModeType), SelectionMode),
                    ButtonCssClasses.CssClass,
                    ButtonCssClasses.CssClassEnabled,
                    ButtonCssClasses.CssClassDisabled,
                    ButtonCssClasses.CssClassSelected,
                    buttonData.ToString());

                string events = (string.IsNullOrEmpty(OnClientButtonClick)) ? "null" : string.Format(_eventsJson, OnClientButtonClick);

                string js =
                @"Sys.Application.add_load({0}_Loader);

                function {0}_Loader() {{                    
                    $create(SCS.Toolbar, {1}, {3}, null, $get(""{2}""));   
                }};";

                js = string.Format(js, this.ClientID, props, this.ClientID, events);
                if (!Page.ClientScript.IsStartupScriptRegistered(this.UniqueID + "_ClientCode"))
                {

                    ScriptManager.RegisterStartupScript(this, typeof(Toolbar), this.UniqueID + "_ClientCode", js, true);
                }
            }
        }

        protected virtual bool MergeClientChanges(string clientChanges)
        {
            bool changed = false;

            foreach (string change in _clientChanges.Split('|'))
            {
                string[] pair = change.Split(',');
                string[] idAndPropertyName = pair[0].Split('~');
                string[] idParts = idAndPropertyName[0].Split('_');

                int index = int.Parse(idParts[idParts.Length - 1]);
                string propertyName = idAndPropertyName[1];
                string propertyValue = pair[1];

                changed = MergeClientChange(Items[index], propertyName, propertyValue);
            }
            return changed;
        }
        protected virtual bool MergeClientChange(ToolbarButton button, string property, string value)
        {
            bool changed = false;

            switch (property.ToUpper())
            {
                case "TEXT":
                    if (!value.Equals(button.Text, StringComparison.CurrentCulture))
                        changed = true;

                    button.Text = value;
                    break;

                case "VISIBLE":
                    bool newVisible = Convert.ToBoolean(value);

                    if (!value.Equals(newVisible))
                        changed = true;

                    button.Visible = newVisible;
                    break;

                case "ENABLED":
                    bool newEnabled = Convert.ToBoolean(value);

                    if (!value.Equals(newEnabled))
                        changed = true;

                    button.Enabled = newEnabled;
                    break;

                case "SELECTED":
                    bool newSelected = Convert.ToBoolean(value);

                    if (!value.Equals(newSelected))
                        changed = true;

                    button.Selected = newSelected;
                    break;

                case "CSS":
                    if (!value.Equals(button.CssClass, StringComparison.CurrentCultureIgnoreCase))
                        changed = true;

                    button.CssClass = value;
                    break;


                case "CSSENABLED":
                    if (!value.Equals(button.CssClassEnabled, StringComparison.CurrentCultureIgnoreCase))
                        changed = true;

                    button.CssClassEnabled = value;
                    break;

                case "CSSDISABLED":
                    if (!value.Equals(button.CssClassDisabled, StringComparison.CurrentCultureIgnoreCase))
                        changed = true;

                    button.CssClassDisabled = value;
                    break;

                case "CSSSELECTED":
                    if (!value.Equals(button.CssClassSelected, StringComparison.CurrentCultureIgnoreCase))
                        changed = true;

                    button.CssClassSelected = value;
                    break;
            }
            return changed;
        }

        #region Properties

        #region Misc
        [Category("Misc"), Description("The collection of toolbar items."), Bindable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content), NotifyParentProperty(true)]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        public ToolbarItemCollection Items
        {
            get
            {
                if (_items == null)
                {
                    _items = new ToolbarItemCollection();
                    _items.ToolbarParent = this;

                    if (IsTrackingViewState)
                    {
                        ((IStateManager)_items).TrackViewState();
                    }
                }
                return _items;
            }
        }

        private bool _enableClientApi = false;
        [Category("Misc"), Description("Turns on and off the client side API."), Bindable(false), DefaultValue(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content), NotifyParentProperty(true)]
        [PersistenceMode(PersistenceMode.Attribute)]
        public bool EnableClientApi
        {
            get
            {

                return _enableClientApi;
            }
            set
            {
                _enableClientApi = value;
            }
        }
        #endregion

        #region Behavior
        string _behaviorId = "ToolbarClient";
        [Category("Behavior"), Description("The ID used to reference the toolbar on the client."), Bindable(false), DefaultValue("ToolbarClient")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content), NotifyParentProperty(true)]
        [PersistenceMode(PersistenceMode.Attribute)]
        public string BehaviorId
        {
            get
            {
                return _behaviorId;
                
                
            }
            set
            {
                _behaviorId = value;
            }
        }

        private string _onClientButtonClick=string.Empty;
        [Category("Behavior"), Description("A JavaScript function on the client used to handle button clicks prior to postback. The event can be canceled so that postback does not occur."), Bindable(true), DefaultValue("")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content), NotifyParentProperty(true)]
        [PersistenceMode(PersistenceMode.Attribute)]
        public string OnClientButtonClick
        {
            get
            {

                return _onClientButtonClick;
            }
            set
            {
                _onClientButtonClick = value;
            }
        }

        private SelectionModeType _selectionMode = SelectionModeType.Single;
        [Category("Behavior"), Description("Determinds whether one or multiple options can remain selected at one time."), Bindable(true), DefaultValue(SelectionModeType.Off)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content), NotifyParentProperty(true)]
        [PersistenceMode(PersistenceMode.Attribute)]
        public SelectionModeType SelectionMode
        {
            get
            {

                return _selectionMode;
            }
            set
            {
                _selectionMode = value;
            }
        }

        #endregion

        #region Appearance
        [Category("Appearance"), Description("The style to apply to each button."), Bindable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content), NotifyParentProperty(true)]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        public ButtonClasses ButtonCssClasses
        {
            get
            {
                if (_buttonStyle == null)
                    _buttonStyle = new ButtonClasses();

                return _buttonStyle;
            }
        }
        #endregion
        int _lastSelectedIndex=-1;
        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int LastSelectedIndex
        {
            get
            {

                return _lastSelectedIndex;
            }
            set
            {
                _lastSelectedIndex = value;
            }
        }

        #endregion

        #region View State

        protected override object SaveViewState()
        {
            object[] state = new object[2];

            state[0] = base.SaveViewState();
            state[1] = (_items != null) ? ((IStateManager)_items).SaveViewState() : null;

            // Another perfomance optimization. If no modifications were made to any
            // properties from their persisted state, the view state for this control
            // is null. Returning null, rather than an array of null values helps
            // minimize the view state significantly.

            for (int i = 0; i < 2; i++)
                if (state[i] != null)
                    return state;

            return null;
        }
        protected override void LoadViewState(object savedState)
        {
            object baseState = null;
            object[] state = null;

            if (savedState != null)
            {
                state = (object[])savedState;
                baseState = state[0];
            }

            // Always call the base class, even if the state is null, so
            // the base class gets a chance to fully implement its LoadViewState
            // functionality.
            base.LoadViewState(baseState);

            if (state == null)
                return;

            if (state[1] != null)
                ((IStateManager)Items).LoadViewState(state[1]);
        }
        protected override void TrackViewState()
        {
            base.TrackViewState();

            if (_items != null)
                ((IStateManager)_items).TrackViewState();
        }

        #endregion

        #region IPostBackDataHandler Members

        public bool LoadPostData(string postDataKey, NameValueCollection postCollection)
        {
            _clientChanges = postCollection[postDataKey];

            if (_clientChanges.Length > 0)
                return (MergeClientChanges(_clientChanges));

            return false;
        }
        public void RaisePostDataChangedEvent()
        {
            OnClientChange(new EventArgs());
        }

        #endregion
    }
}
