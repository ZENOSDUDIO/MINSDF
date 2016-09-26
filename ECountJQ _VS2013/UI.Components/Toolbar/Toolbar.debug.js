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

/// <reference name="MicrosoftAjax.debug.js" />
/// <reference name="MicrosoftAjaxWebForms.debug.js" />

Type.registerNamespace("SCS");

/* Toolbar Class
/*******************************************************************************************************/

SCS.Toolbar = function(element) {
    SCS.Toolbar.initializeBase(this, [element]);

    /* Toolbar Fields
    *******************************************************************************************************/

    this._buttons = [];
    this._itemData = {};
    this._selectedIndex = -1;
    this._lastSelectedIndex = -1;
    this._btnCss = "";
    this._btnCssEnabled = "";
    this._btnCssDisabled = "";
    this._btnCssSelected = "";
    this._selectionMode = "Off";

    this._buttonClickedHandler;
    this._util;
};

SCS.Toolbar.prototype = {

    /* Toolbar Setup
    *******************************************************************************************************/

    initialize: function() {
        SCS.Toolbar.callBaseMethod(this, 'initialize');

        this._buttonClickedHandler = Function.createDelegate(this, this._button_onClick);
        this._util = new SCS.Util();

        // load child objects
        var nodes = this.get_element().childNodes;

        for (var i = 0; i < nodes.length; i++) {
            var node = nodes[i];

            if (node.nodeType != 3 && node.tagName == "DIV")
                this._addButton(node, this._itemData[i], i);
        }
    },

    /* Toolbar Teardown
    ********************************************************************************************************/

    dispose: function() {
        this._itemData = null;
        this._buttons = null;
        this._buttonClickedHandler = null;
        this._util = null;

        SCS.Toolbar.callBaseMethod(this, 'dispose');
    },

    /* Toolbar Public Methods
    *******************************************************************************************************/

    getButtonByIndex: function(index) {
        return this._buttons[index];
    },
    getButtonById: function(clientId) {

        for (var i = 0; i < this._buttons.length; i++)
            if (this._buttons[i].get_element().id === clientId)
            return this._buttons[i];

        return null;
    },
    getButtonByText: function(text) {

        text = text.toUpperCase();
        for (var i = 0; i < this._buttons.length; i++)
            if (this._buttons[i].get_text().toUpperCase() === text)
                return this._buttons[i];

        return null;
    },
    setItemSelected: function(index, selected) {

        var mode = this._selectionMode;

        if (mode != "Multiple")
            this.clearSelections();

        if (mode != "Off") {
            var btn = this._buttons[index];

            if (mode == "Single")
                btn._selected = selected;
            else
                btn._selected = !btn._selected; // toggle selection

            btn._setSelectedCss(btn._selected);

            if (btn._selected)
                this._lastSelectedIndex = index;
        }
    },
    clearSelections: function() {

        for (var i = 0; i < this._buttons.length; i++) {
            this._buttons[i]._selected = false;
            this._buttons[i]._setSelectedCss(false);
        }
    },

    /* Toolbar Private Methods
    *******************************************************************************************************/

    _addButton: function(wrapper, data, index) {

        var params = { "parent": this, "setupData": data, "index": index };
        var events = { "clicked": this._buttonClickedHandler };

        data.css = this._util.getProp(data, "css", this._btnCss);
        data.cssEnabled = this._util.getProp(data, "cssEnabled", this._btnCssEnabled);
        data.cssDisabled = this._util.getProp(data, "cssDisabled", this._btnCssDisabled);
        data.cssSelected = this._util.getProp(data, "cssSelected", this._btnCssSelected);

        // Create the new button object
        var btn = $create(SCS.ToolbarButton, params, events, null, wrapper);

        // Add the new button
        Array.add(this._buttons, btn);

        return btn;
    },


    /* Toolbar Event Handlers
    *******************************************************************************************************/

    _button_onClick: function(source, args) {
        args.set_oldIndex(this._selectedIndex);
        this.raiseButtonClicked(args);
    },

    /* Toolbar Events
    *******************************************************************************************************/

    add_buttonClicked: function(handler) {
        this.get_events().addHandler('buttonClicked', handler);
    },
    remove_buttonClicked: function(handler) {
        this.get_events().removeHandler('buttonClicked', handler);
    },
    raiseButtonClicked: function(eventArgs) {
        var handler = this.get_events().getHandler('buttonClicked');
        if (handler) {
            handler(this, eventArgs);

            if (!eventArgs.get_cancel())
                this._selectedIndex = eventArgs.get_selectedIndex();
        }
    },

    /* Toolbar Properties
    *******************************************************************************************************/

    get_items: function() {
        return this._buttons;
    },
    get_itemData: function() {
        return this._itemData;
    },
    set_itemData: function(value) {
        this._itemData = value;
    },
    get_selectedIndex: function() {
        return this._selectedIndex;
    },
    get_buttonCssClass: function() {
        return this._btnCss;
    },
    set_buttonCssClass: function(value) {
        this._btnCss = value;
    },
    get_buttonCssClassEnabled: function() {
        return this._btnCssEnabled;
    },
    set_buttonCssClassEnabled: function(value) {
        this._btnCssEnabled = value;
    },
    get_buttonCssClassDisabled: function() {
        return this._btnCssDisabled;
    },
    set_buttonCssClassDisabled: function(value) {
        this._btnCssDisabled = value;
    },
    get_buttonCssClassSelected: function() {
        return this._btnCssSelected;
    },
    set_buttonCssClassSelected: function(value) {
        this._btnCssSelected = value;
    },
    get_selectionMode: function() {
        return this._selectionMode;
    },
    set_selectionMode: function(value) {
        this._selectionMode = value;
    }
};

SCS.Toolbar.registerClass('SCS.Toolbar', Sys.UI.Behavior);

/* Toolbar Button Class
 *******************************************************************************************************/

SCS.ToolbarButton = function(element) {
    SCS.ToolbarButton.initializeBase(this, [element]);

    /* Button Fields
    *******************************************************************************************************/

    this._index = -1;
    this._parent;
    this._util;
    this._anchor;
    this._confirmMsg = "";
    this._css = "";
    this._cssEnabled = "";
    this._cssDisabled = "";
    this._cssSelected = "";
    this._enabled = true;
    this._selected = false;
    this._setupData = "";
    this._isInit = true;
    this._imageUrl = "";
    this._disabledImageUrl = "";

    this._confirmHandler;
};
SCS.ToolbarButton.prototype = {

    /* Button Setup
    *******************************************************************************************************/

    initialize: function() {
        SCS.ToolbarButton.callBaseMethod(this, 'initialize');

        this._util = this._parent._util;

        // use local varriable to improve crunch compression
        var setupData = this._setupData;

        // set confirmation message
        this._confirmMsg = this._util.getProp(setupData, "confirmMessage", "");

        // set css classes
        this._css = setupData.css;
        this._cssEnabled = setupData.cssEnabled;
        this._cssDisabled = setupData.cssDisabled;
        this._cssSelected = setupData.cssSelected;

        // discover url and keep copy
        this._anchor = this._util.getFirstChild(this.get_element());

        // set state of button
        this.set_enabled(setupData.enabled);

        //alert(this._enabled);

        // discover or set image urls
        if (this._enabled) {

            this._disabledImageUrl = this._util.getProp(setupData, "disabledImageUrl", "");
            this._imageUrl = this._anchor.style.backgroundImage;
        }
        else {
            this._disabledImageUrl = this._anchor.style.backgroundImage;
            this._imageUrl = this._util.getProp(setupData, "imageUrl", "");
        }

        // wire-up click handler
        this._confirmHandler = Function.createDelegate(this, this._onButtonClick);
        $addHandler(this.get_element(), "click", this._confirmHandler);

        this._setupData = null;
        this._clearChanges();
        this._isInit = false;
    },

    /* Button Teardown
    *******************************************************************************************************/

    dispose: function() {
        $removeHandler(this.get_element(), "click", this._confirmHandler);
        this._confirmHandler = null;

        this._util = null;

        SCS.ToolbarButton.callBaseMethod(this, 'dispose');
    },

    /* Button Public Methods
    *******************************************************************************************************/


    /* Button Private Methods
    *******************************************************************************************************/

    _saveChange: function(propName, propValue) {

        if (this._isInit)
            return;

        var stateHolder = this._util.getSibling(this.get_element(), "input", 100);
        var stateArray = this._removeChange(stateHolder.value, propName);

        var propPair = String.format("{0}~{1},{2}", this.get_element().id, propName, propValue);

        Array.add(stateArray, propPair);
        stateHolder.value = stateArray.join("|");
    },
    _removeChange: function(states, propName) {
        if (!states)
            return [];

        var stateArray = states.split("|");
        var id = this.get_element().id;

        for (var i = 0; i < stateArray.length; i++) {
            if (stateArray[i].split(",")[0] == (id + "~" + propName)) {
                Array.remove(stateArray, stateArray[i]);
                break;
            }
        }
        return stateArray;
    },
    _clearChanges: function() {
        var stateHolder = this._util.getSibling(this.get_element(), "input", 100);
        stateHolder.value = "";
    },
    _swapClasses: function(prevClass, newClass) {

        if (this._isInit)
            return;

        var elem = this.get_element();
        Sys.UI.DomElement.removeCssClass(elem, prevClass);
        Sys.UI.DomElement.addCssClass(elem, newClass);
    },
    _setImage: function(enabled) {

        if (this._isInit || this._disabledImageUrl.length == 0)
            return;

        var url = (enabled) ? this._imageUrl : this._disabledImageUrl;

        if (url.substring(0, 3).toUpperCase() != "URL")
            url = String.format("url({0})", url);

        this._anchor.style.backgroundImage = url;
    },
    _setSelectedCss: function(selected) {

        try {
            var cls_add = (selected) ? this._cssSelected : this._cssEnabled;
            var cls_rem = (selected) ? this._cssEnabled : this._cssSelected;

            this._swapClasses(cls_rem, cls_add);
        }
        catch (e) { }
    },
    _setEnabled: function(enabled) {

        try {
            var cls_add = (enabled) ? this._cssEnabled : this._cssDisabled;
            var cls_rem = (enabled) ? this._cssDisabled : this._cssEnabled;

            this._swapClasses(cls_rem, cls_add);
        }
        catch (e) { }

        this._setImage(enabled);

        var lnk = this._anchor;

        if (lnk.title.length > 0) {
            if (enabled)
                lnk.title = lnk.title.replace(" (disabled)", "");
            else
                lnk.title += " (disabled)";
        }

        lnk.disabled = !enabled;
        lnk.href = (!enabled) ? "javascript:void(0);" : lnk.href;
    },

    /* Button Events
    /*******************************************************************************************************/

    add_clicked: function(handler) {
        this.get_events().addHandler('clicked', handler);
    },
    remove_clicked: function(handler) {
        this.get_events().removeHandler('clicked', handler);
    },
    raiseClicked: function(eventArgs) {

        var handler = this.get_events().getHandler('clicked');
        if (handler)
            handler(this, eventArgs);
    },
    /* Button Event Handlers
    *******************************************************************************************************/

    _onButtonClick: function(e) {

        if (!this.get_enabled())
            return false;

        var eventArgs = new SCS.ToolbarClickEventArgs(-1, this._index);
        this.raiseClicked(eventArgs);

        if (eventArgs.get_cancel())
            return false;

        if (this._confirmMsg.length > 0 && !confirm(this._confirmMsg))
            return false;

        return true;
    },

    /* Button Properties
    *******************************************************************************************************/

    get_setupData: function() {
        return this._setupData;
    },
    set_setupData: function(value) {
        this._setupData = value;
    },
    get_parent: function() {
        return this._parent;
    },
    set_parent: function(value) {
        this._parent = value;
    },
    get_text: function() {
        return this._util.getFirstChild(this.get_element()).innerHTML;
    },
    set_text: function(value) {
        var elem = this._util.getFirstChild(this.get_element());
        var prevValue = elem.innerHTML;

        if (value.trim().length < 1)
            value = "&nbsp;";

        elem.innerHTML = value;

        if (prevValue.toUpperCase() != value.toUpperCase())
            this._saveChange("Text", value);
    },
    get_index: function() {
        return this._index;
    },
    set_index: function(value) {
        this._index = value;
    },
    get_cssClass: function() {
        return this._css;
    },
    set_cssClass: function(value) {
        var prevValue = this._css;
        this._css = value;

        this._swapClasses(prevValue, value);

        if (this._css != prevValue)
            this._saveChange("Css", value);
    },
    get_cssClassEnabled: function() {
        return this._cssEnabled;
    },
    set_cssClassEnabled: function(value) {
        var prevValue = this._cssEnabled;
        this._cssEnabled = value;

        if (this.get_enabled())
            this._swapClasses(prevValue, value);

        if (this._cssEnabled != prevValue)
            this._saveChange("CssEnabled", value);
    },
    get_cssClassDisabled: function() {
        return this._cssDisabled;
    },
    set_cssClassDisabled: function(value) {
        var prevValue = this._cssDisabled;
        this._cssDisabled = value;

        this._swapClasses(prevValue, value);

        if (this._cssDisabled != prevValue)
            this._saveChange("CssDisabled", value);
    },
    get_cssClassSelected: function() {
        return this._cssSelected;
    },
    set_cssClassSelected: function(value) {
        var prevValue = this._cssSelected;
        this._cssSelected = value;

        this._swapClasses(prevValue, value);

        if (this._cssSelected != prevValue)
            this._saveChange("CssSelected", value);
    },
    get_enabled: function() {
        return this._enabled;
    },
    set_enabled: function(value) {
        var prevValue = this.get_enabled();
        this._enabled = value;

        this._setEnabled(value);

        if (prevValue != this._enabled)
            this._saveChange("Enabled", value);
    },
    get_visible: function() {
        return Sys.UI.DomElement.getVisible(this.get_element());
    },
    set_visible: function(value) {
        var prevValue = this.get_visible();

        Sys.UI.DomElement.setVisible(this.get_element(), value);

        if (prevValue != this.get_visible())
            this._saveChange("Visible", value);
    },
    get_selected: function() {

        return this._selected;
    },
    set_selected: function(value) {

        if (!this._enabled) {
            alert("Item cannot be selected because it is disabled.");
            return;
        }

        var prevValue = this._selected;

        this._parent.setItemSelected(this._index, value);

        if (prevValue != this.get_selected())
            this._saveChange("selected", value);
    },
    get_imageUrl: function() {

        return this._imageUrl;
    },
    set_imageUrl: function(value) {

        var prevValue = this._imageUrl;

        if (prevValue != this.get_imageUrl())
            this._saveChange("imageUrl", value);
    },
    get_disabledImageUrl: function() {

        return this._disabledImageUrl;
    },
    set_disabledImageUrl: function(value) {

        var prevValue = this._disabledImageUrl;

        if (prevValue != this.get_disabledImageUrl())
            this._saveChange("disabledImageUrl", value);
    }
};
SCS.ToolbarButton.registerClass('SCS.ToolbarButton', Sys.UI.Behavior);

/* ToolbarClickEventArgs
 *******************************************************************************************************/

SCS.ToolbarClickEventArgs = function(oldIndex, selectedIndex) {

    SCS.ToolbarClickEventArgs.initializeBase(this);

    this._oldIndex = oldIndex;
    this._selectedIndex = selectedIndex;
};

SCS.ToolbarClickEventArgs.prototype = {
    get_oldIndex: function() {
        return this._oldIndex;
    },
    set_oldIndex: function(value) {
        this._oldIndex = value;
    },
    get_selectedIndex: function() {
        return this._selectedIndex;
    },
    set_selectedIndex: function(value) {
        this._selectedIndex = value;
    }
};

SCS.ToolbarClickEventArgs.registerClass('SCS.ToolbarClickEventArgs', Sys.CancelEventArgs);

/* Util
*******************************************************************************************************/

SCS.Util = function() {

};

SCS.Util.prototype = {

    getFirstChild: function(obj) {

        obj = obj.firstChild;
        while (obj && obj.nodeType != 1)
            obj = obj.nextSibling;

        return obj;
    },
    getNextSibling: function(obj) {

        obj = obj.nextSibling;
        while (obj && obj.nodeType != 1)
            obj = obj.nextSibling;

        return obj;
    },
    getSibling: function(obj, tagName, numChecks) {

        if (obj == null)
            return null;

        tagName = tagName.toUpperCase();

        for (var i = 0; i < numChecks; i++) {
            obj = obj.nextSibling;

            // skip text nodes
            if (obj.nodeType == 3)
                obj = obj.nextSibling;

            if (obj == null)
                break;

            if (obj.tagName.toUpperCase() == tagName)
                return obj;
        }
        return null;
    },
    getProp: function(obj, prop, def) {

        var val;
        eval("try {val=obj." + prop + "}catch(e){}");

        return (typeof (val) != "undefined") ? val : def;
    }
};

if (typeof(Sys) !== 'undefined') Sys.Application.notifyScriptLoaded();