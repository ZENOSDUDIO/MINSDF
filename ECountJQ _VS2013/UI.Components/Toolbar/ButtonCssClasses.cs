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
using System.Linq;
using System.Security.Permissions;
using System.Web;
using System.Web.UI;
using System.Web.UI.Design;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

using SCS.Web.UI.WebControls.Design;

namespace SCS.Web.UI.WebControls
{
    [TypeConverter(typeof(ButtonCssClassConverter))]
    public class ButtonClasses : IStateManager
    {
        #region Fields

        private bool _isTrackingViewState = false;
        private StateBag _viewState;

        private string _cssClass;
        private string _cssClassEnabled;
        private string _cssClassDisabled;
        private string _cssClassSelected;

        #endregion

        public ButtonClasses()
        {
        }
        public ButtonClasses(string cssClassEnabled, string cssClassHover)
        {
            CssClassEnabled = cssClassEnabled;
        }
        public ButtonClasses(string cssClassEnabled, string cssClassDisabled, string cssClassSelected, string cssClassHover)
        {
            CssClassEnabled = cssClassEnabled;
            CssClassDisabled = CssClassDisabled;
            CssClassSelected = cssClassSelected;
        }

        #region IStateManager Members

        bool IStateManager.IsTrackingViewState
        {
            get
            {
                return _isTrackingViewState;
            }
        }
        void IStateManager.LoadViewState(object state)
        {
            if (state != null)
            {
                if (state != null)
                    ((IStateManager)ViewState).LoadViewState(state);

                object cssClass = ViewState["cssClass"];

                if (cssClass != null)
                    this.CssClass = (string)cssClass;

                object cssClassEnabled = ViewState["cssClassEnabled"];

                if (cssClassEnabled != null)
                    this.CssClassEnabled = (string)cssClassEnabled;

                object cssClassSelected = ViewState["cssClassSelected"];

                if (cssClassSelected != null)
                    this.CssClassSelected = (string)cssClassSelected;

                object cssClassDisabled = ViewState["cssClassDisabled"];

                if (cssClassDisabled != null)
                    this.CssClassDisabled = (string)cssClassDisabled;
            }
        }
        object IStateManager.SaveViewState()
        {
            string currentCssClass = this.CssClass;
            string initialCssClass = (ViewState["CssClass"] == null) ? "" : (string)ViewState["CssClass"];

            if (currentCssClass.Equals(initialCssClass) == false)
            {
                ViewState["CssClass"] = currentCssClass;
            }

            string currentCssClassEnabled = this.CssClassEnabled;
            string initialCssClassEnabled = (ViewState["CssClassEnabled"] == null) ? "" : (string)ViewState["CssClassEnabled"];

            if (currentCssClassEnabled.Equals(initialCssClassEnabled) == false)
            {
                ViewState["CssClassEnabled"] = currentCssClassEnabled;
            }

            string currentCssClassSelected = this.CssClassSelected;
            string initialCssClassSelected = (ViewState["CssClassSelected"] == null) ? "" : (string)ViewState["CssClassSelected"];

            if (currentCssClassSelected.Equals(initialCssClassSelected) == false)
            {
                ViewState["CssClassSelected"] = currentCssClassSelected;
            }

            string currentCssClassDisabled = this.CssClassDisabled;
            string initialCssClassDisabled = (ViewState["CssClassDisabled"] == null) ? "" : (string)ViewState["CssClassDisabled"];

            if (currentCssClassDisabled.Equals(initialCssClassDisabled) == false)
            {
                ViewState["CssClassDisabled"] = currentCssClassDisabled;
            }          

            if (_viewState != null)
            {
                return ((IStateManager)_viewState).SaveViewState();
            }
            return null;
        }
        void IStateManager.TrackViewState()
        {
            if (CssClass.Length > 0)
                ViewState["CssClass"] = _cssClass;

            if (CssClassEnabled.Length > 0)
                ViewState["CssClassEnabled"] = _cssClassEnabled;

            if (CssClassSelected.Length > 0)
                ViewState["CssClassSelected"] = _cssClassSelected;

            if (CssClassDisabled.Length > 0)
                ViewState["CssClassDisabled"] = _cssClassDisabled;
                       
            _isTrackingViewState = true;

            if (_viewState != null)
            {
                ((IStateManager)_viewState).TrackViewState();
            } 
        }

        #endregion

        #region Properties

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

        [NotifyParentProperty(true), DefaultValue("")]
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

        [NotifyParentProperty(true), DefaultValue("")]
        public string CssClassEnabled
        {
            get
            {
                return _cssClassEnabled;
            }
            set
            {
                _cssClassEnabled = value;
            }
        }

        [NotifyParentProperty(true), DefaultValue("")]
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

        [NotifyParentProperty(true), DefaultValue("")]
        public string CssClassDisabled
        {
            get
            {
                return _cssClassDisabled;
            }
            set
            {
                _cssClassDisabled = value;
            }
        }

        #endregion
    }
}
