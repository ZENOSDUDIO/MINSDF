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
using System.ComponentModel;
using System.Drawing.Design;
using System.Linq;
using System.Security.Permissions;
using System.Web;
using System.Web.UI;
using System.Web.UI.Design;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace SCS.Web.UI.WebControls
{
    public class ToolbarButton : IStateManager
    {
        #region Fields
        private LinkButton _linkButton = new LinkButton();

        private bool _isTrackingViewState = false;
        private StateBag _viewState;

        private bool _visible = true;
        private bool _enabled = true;
        private bool _selected = false;
        private bool _postBack = true;
        
        private string _imageUrl = "";
        private string _disabledImageUrl = "";
        
        private string _cssClass = "";
        private string _cssClassSelected = "";
        private string _enabledCssClass = "";
        private string _disabledCssClass = "";
        
        private string _confirmMessage = "";

        private int _index = -1;
        #endregion

        #region Constructors
        public ToolbarButton()
        {
        }
        public ToolbarButton(string imageUrl)
        {
            ImageUrl = imageUrl;
        }
        public ToolbarButton(string imageUrl, string text)
        {
            Text = text;
            ImageUrl = imageUrl;
        }
        public ToolbarButton(string imageUrl, string text, ButtonClasses cssClasses)
        {
            Text = text;
            ImageUrl = imageUrl;
        
            CssClass = cssClasses.CssClass;
            CssClassEnabled = cssClasses.CssClassEnabled;
            CssClassDisabled = cssClasses.CssClassDisabled;
            CssClassSelected = cssClasses.CssClassSelected;
        }
        public ToolbarButton(string imageUrl, string text, ButtonClasses cssClasses, bool enabled, bool visible, bool selected)
        {
            Text = text;
            ImageUrl = imageUrl;
            Enabled = enabled;
            Visible = visible;
            Selected = selected;

            CssClass = cssClasses.CssClass;
            CssClassEnabled = cssClasses.CssClassEnabled;
            CssClassDisabled = cssClasses.CssClassDisabled;
            CssClassSelected = cssClasses.CssClassSelected;
        }
        #endregion

        internal void RenderButton(HtmlTextWriter writer, int index)
        {
            string id = ToolbarParent.ClientID + "_item_" + index.ToString();
            writer.AddAttribute(HtmlTextWriterAttribute.Id, id);

            RenderButtonWrapperAttributes(writer);
            writer.RenderBeginTag(HtmlTextWriterTag.Div);

            if (BaseLinkButton.Text == "")
                BaseLinkButton.Text = "&nbsp;"; // required for image to show

            RenderButtonLinkAttributes(writer);

            //if (!PostBack)
            //    BaseLinkButton.PostBackUrl = "javascript:void(0);";

            BaseLinkButton.RenderControl(writer);

            writer.RenderEndTag();  
        }

        protected virtual void RenderButtonWrapperAttributes(HtmlTextWriter writer)
        {
            string allClass = (!string.IsNullOrEmpty(CssClass))
                ? CssClass : ToolbarParent.ButtonCssClasses.CssClass;
                        
            string subClass = "";

            if (Enabled)
            {
                if (Selected)
                {
                    subClass = (!string.IsNullOrEmpty(CssClassSelected))
                        ? CssClassSelected : ToolbarParent.ButtonCssClasses.CssClassSelected;                
                }
                
                if (Enabled && subClass.Length == 0)
                {
                    subClass = (!string.IsNullOrEmpty(CssClassEnabled))
                        ? CssClassEnabled : ToolbarParent.ButtonCssClasses.CssClassEnabled;
                }
            }            
            else
            {
                subClass = (!string.IsNullOrEmpty(CssClassDisabled))
                    ? CssClassDisabled : ToolbarParent.ButtonCssClasses.CssClassDisabled; ;
            }

            writer.AddAttribute(HtmlTextWriterAttribute.Class,
                allClass + " " + subClass);

            if (!this.Visible)
                writer.AddStyleAttribute(HtmlTextWriterStyle.Display, "none");
        }
        protected virtual void RenderButtonLinkAttributes(HtmlTextWriter writer)
        {
            string imageUrl = (Enabled) ? this.ImageUrl : this.DisabledImageUrl;

            if (!string.IsNullOrEmpty(imageUrl))
            {
                imageUrl = ToolbarParent.Page.ResolveClientUrl(imageUrl);

                BaseLinkButton.Style.Add("background-image", string.Format("url({0})", imageUrl));
                BaseLinkButton.Style.Add("background-repeat", "no-repeat");
                BaseLinkButton.Style.Add("background-position", "3px 3px");
            }
        }

        #region View State
        protected StateBag ViewState
        {
            get
            {
                if (_viewState == null)
                {
                    _viewState = new StateBag(false);
                    
                    if (_isTrackingViewState) 
                        ((IStateManager)_viewState).TrackViewState();
                }
                return _viewState;
            }
        }
        internal void SetDirty()
        {
            if (_viewState != null)
            {
                ICollection Keys = _viewState.Keys;
                foreach (string key in Keys)
                {
                    _viewState.SetItemDirty(key, true);
                }
            }
        }

        #region IStateManager implementation

        bool IStateManager.IsTrackingViewState
        {
            get
            {
                return _isTrackingViewState;
            }
        }
        void IStateManager.LoadViewState(object savedState)
        {
            if (savedState != null)
            {
                ((IStateManager)ViewState).LoadViewState(savedState);

                object text = ViewState["text"];

                if (text != null)
                    this.Text = (string)text;

                object cssClass = ViewState["cssClass"];

                if (cssClass != null)
                    this.CssClass = (string)cssClass;

                object enabledCssClass = ViewState["enabledCssClass"];

                if (enabledCssClass != null)
                    this.CssClassEnabled = (string)enabledCssClass;

                object disabledCssClass = ViewState["disabledCssClass"];

                if (disabledCssClass != null)
                    this.CssClassDisabled = (string)disabledCssClass;

                object imageUrl = ViewState["imageUrl"];

                if (imageUrl != null)
                    this.ImageUrl = (string)imageUrl;

                object disabledImageUrl = ViewState["disabledImageUrl"];

                if (disabledImageUrl != null)
                    this.DisabledImageUrl = (string)disabledImageUrl;

                object enabled = ViewState["enabled"];

                if (enabled != null)
                    this.Enabled = (bool)enabled;

                //object postback = ViewState["postback"];

                //if (postback != null)
                //    this.PostBack = (bool)postback;

                object confirmMessage = ViewState["confirmMessage"];

                if (confirmMessage != null)
                    this.ConfirmationMessage = (string)confirmMessage;

                object visible = ViewState["visible"];

                if (visible != null)
                    this.Visible = (bool)visible;

                object selected = ViewState["selected"];

                if (selected != null)
                    this.Selected = (bool)selected;
                
            }
        }
        object IStateManager.SaveViewState()
        {
            string currentText = this.Text;
            string initialText = (ViewState["text"] == null) ? "" : (string)ViewState["text"];

            if (currentText.Equals(initialText) == false)
            {
                ViewState["text"] = currentText;
            }

            string currentCssClass = this.CssClass;
            string initialCssClass = (ViewState["cssClass"] == null) ? "" : (string)ViewState["cssClass"];

            if (currentCssClass.Equals(initialCssClass) == false)
            {
                ViewState["cssClass"] = currentCssClass;
            }

            string currentDisabledCssClass = this.CssClassDisabled;
            string initialDisabledCssClass = (ViewState["disabledCssClass"] == null) ? "" : (string)ViewState["disabledCssClass"];

            if (currentDisabledCssClass.Equals(initialDisabledCssClass) == false)
            {
                ViewState["disabledCssClass"] = currentDisabledCssClass;
            }

            string currentEnabledCssClass = this.CssClassEnabled;
            string initialEnabledCssClass = (ViewState["enabledCssClass"] == null) ? "" : (string)ViewState["enabledCssClass"];

            if (currentEnabledCssClass.Equals(initialEnabledCssClass) == false)
            {
                ViewState["enabledCssClass"] = currentEnabledCssClass;
            }

            string currentImageUrl = this.ImageUrl;
            string initialImageUrl = (ViewState["imageUrl"] == null) ? "" : (string)ViewState["imageUrl"];

            if (currentImageUrl.Equals(initialImageUrl) == false)
            {
                ViewState["imageUrl"] = currentImageUrl;
            }

            string currentDisabledImageUrl = this.DisabledImageUrl;
            string initialDisabledImageUrl = (ViewState["disabledImageUrl"] == null) ? "" : (string)ViewState["disabledImageUrl"];

            if (currentDisabledImageUrl.Equals(initialDisabledImageUrl) == false)
            {
                ViewState["disabledImageUrl"] = currentDisabledImageUrl;
            }

            bool currentEnabled = this.Enabled;
            bool initialEnabled = (ViewState["enabled"] == null) ? true : (bool)ViewState["enabled"];

            if (currentEnabled.Equals(initialEnabled) == false)
            {
                ViewState["enabled"] = currentEnabled;
            }

            //bool currentPostBack = this.PostBack;
            //bool initialPostBack = (ViewState["postBack"] == null) ? true : (bool)ViewState["postBack"];

            //if (currentPostBack.Equals(initialPostBack) == false)
            //{
            //    ViewState["postBack"] = currentPostBack;
            //}

            string currentConfirmMessage = this.ConfirmationMessage;
            string initialConfirmaMessage = (ViewState["confirmationMessage"] == null) ? "" : (string)ViewState["confirmationMessage"];

            if (currentConfirmMessage.Equals(initialConfirmaMessage) == false)
            {
                ViewState["confirmationMessage"] = currentConfirmMessage;
            }

            bool currentVisible = this.Visible;
            bool initialVisible = (ViewState["visible"] == null) ? true : (bool)ViewState["visible"];

            if (currentVisible.Equals(initialVisible) == false)
            {
                ViewState["visible"] = currentVisible;
            }

            bool currentSelected = this.Selected;
            bool initialSelected = (ViewState["selected"] == null) ? true : (bool)ViewState["selected"];

            if (currentSelected.Equals(initialSelected) == false)
            {
                ViewState["selected"] = currentSelected;
            }

            if (_viewState != null)
            {
                return ((IStateManager)_viewState).SaveViewState();
            }
            return null;
        }
        void IStateManager.TrackViewState()
        {
            if (_linkButton.Text.Length > 0)
                ViewState["text"] = _linkButton.Text;

            if (CssClassSelected.Length > 0)
                ViewState["cssClass"] = _cssClass;

            if (CssClassDisabled.Length > 0)
                ViewState["disabledCssClass"] = _disabledCssClass;

            if (CssClassEnabled.Length > 0)
                ViewState["enabledCssClass"] = _enabledCssClass;

            if (ImageUrl.Length > 0)
                ViewState["imageUrl"] = ImageUrl;

            if (DisabledImageUrl.Length > 0)
                ViewState["imageUrl"] = DisabledImageUrl;

            if (Enabled != true)
                ViewState["enabled"] = Enabled;

            //if (PostBack != true)
            //    ViewState["postBack"] = PostBack;

            if (ConfirmationMessage.Length > 0)
                ViewState["confirmationMessage"] = ConfirmationMessage;

            if (Visible != true)
                ViewState["visible"] = Visible;

            if (Selected != false)
                ViewState["selected"] = Selected;

            _isTrackingViewState = true;

            if (_viewState != null)
            {
                ((IStateManager)_viewState).TrackViewState();
            }
        }

        #endregion
        #endregion

        #region Properties

        internal LinkButton BaseLinkButton
        {
            get { return _linkButton; }
        }

        internal Toolbar ToolbarParent
        {
            get;
            set;
        }

        #region Appearance
        [Bindable(true), Category("Appearance"), DefaultValue(""), Themeable(true), Description("The URL of the button image.")]
        [Editor(typeof(UrlEditor), typeof(UITypeEditor))]
        public string ImageUrl
        {
            get
            {
                return _imageUrl;
            }
            set
            {
                _imageUrl = value;
            }
        }

        [Bindable(true), Category("Appearance"), DefaultValue(""), Themeable(true), Description("The URL of the disabled button image.")]
        [Editor(typeof(UrlEditor), typeof(UITypeEditor))]
        public string DisabledImageUrl
        {
            get
            {
                return _disabledImageUrl;
            }
            set
            {
                _disabledImageUrl = value;
            }
        }

        [Bindable(true), Category("Appearance"), DefaultValue(""), Themeable(false), Description("The text to display on the button.")]
        public string Text
        {
            get 
            { 
                return _linkButton.Text; 
            }
            set 
            { 
                _linkButton.Text = value;
            }
        }

        [Bindable(true), Category("Appearance"), DefaultValue(""), Themeable(true), Description("The CSS class to apply to the toolbar button for all it's states.")]
        public string CssClass
        {
            get
            {
                return _cssClass;
            }
            set
            {
                _cssClass = value;
            }
        }

        [Bindable(true), Category("Appearance"), DefaultValue(""), Themeable(true), Description("The CSS class to apply to the toolbar item when selected.")]
        public string CssClassSelected
        {
            get
            {
                return _cssClassSelected;
            }
            set
            {
                _cssClassSelected = value;
            }
        }

        [Bindable(true), Category("Appearance"), DefaultValue(""), Themeable(true), Description("The CSS class to apply to the toolbar item when enabled.")]
        public string CssClassEnabled
        {
            get
            {
                return _enabledCssClass;
            }
            set
            {
                _enabledCssClass = value;
            }
        }

        [Bindable(true), Category("Appearance"), DefaultValue(""), Themeable(true), Description("The CSS class to apply to the toolbar item when disabled.")]
        public string CssClassDisabled
        {
            get
            {
                return _disabledCssClass;
            }
            set
            {
                _disabledCssClass = value;
            }
        }

        #endregion

        #region Behavior

        [Bindable(true), Category("Behavior"), DefaultValue(true), Themeable(true), Description("The enabled state of the button.")]
        public bool Enabled
        {
            get
            {
                return _enabled;
            }
            set
            {
                _enabled = value;
            }
        }

        [Bindable(true), Category("Behavior"), DefaultValue(true), Themeable(true), Description("The visibility of the button.")]
        public bool Visible
        {
            get
            {
                return _visible;
            }
            set
            {
                _visible = value;
            }
        }

        [Bindable(true), Category("Behavior"), DefaultValue(false), Themeable(true), Description("Whether or not the button is selected.")]
        public bool Selected
        {
            get
            {
                return _selected;
            }
            set
            {
                _selected = value;
            }
        }

        [Bindable(true), Category("Behavior"), DefaultValue(""), Themeable(false), Description("The confirmation message when clicked.")]
        public string ConfirmationMessage
        {
            get
            {
                return _confirmMessage;
            }
            set
            {
                _confirmMessage = value;
            }
        }

        [Bindable(true), Category("Behavior"), DefaultValue(""), Themeable(false), Description("The tooltip message to display.")]
        public string ToolTip
        {
            get
            {
                return _linkButton.ToolTip;
            }
            set
            {
                _linkButton.ToolTip = value;
            }
        }

        [Bindable(true), Category("Behavior"), DefaultValue(""), Themeable(false), Description("The command name to return on postback.")]
        public string CommandName
        {
            get
            {
                return _linkButton.CommandName;
            }
            set
            {
                _linkButton.CommandName = value;
            }
        }

        [Bindable(true), Category("Behavior"), DefaultValue(""), Themeable(false), Description("The command argument to return on postback.")]
        public string CommandArgument
        {
            get
            {
                return _linkButton.CommandArgument;
            }
            set
            {
                _linkButton.CommandArgument = value;
            }
        }

        [Bindable(true), Category("Behavior"), DefaultValue(false), Themeable(false), Description("Whether or not button click triggers form validation.")]
        public bool CausesValidation
        {
            get
            {
                return _linkButton.CausesValidation;
            }
            set
            {
                _linkButton.CausesValidation = value;
            }
        }

        [Bindable(true), Category("Behavior"), DefaultValue(""), Themeable(false), Description("Validation group for which toolbar button triggers during postback.")]
        public string ValidationGroup
        {
            get
            {
                return _linkButton.ValidationGroup;
            }
            set
            {
                _linkButton.ValidationGroup = value;
            }
        }

        //[Bindable(true), Category("Behavior"), DefaultValue(true), Themeable(false), Description("Whether or not the button triggers a post back.")]
        //public bool PostBack
        //{
        //    get
        //    {
        //        return _postBack;
        //    }
        //    set
        //    {
        //        _postBack = value;
        //    }
        //}
        #endregion

        [Bindable(false), Category("Misc"), DefaultValue(""), Themeable(false), Description("The index (position) of the button.")]
        public int Index
        {
            get
            {
                return _index;
            }
            internal set
            {
                _index = value;
            }
        }
        #endregion
    }
}
