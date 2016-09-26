using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SGM.ECount.DataModel;

public partial class BizDataMaintain_ConsignmentPartRecordEdit : ECountBasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request.QueryString["recordid"] != null)
            {
                bindBaseData(Request.QueryString["recordid"].ToString());
            }
        }
        this.txtSupplier.Text = hidSupplierDUNS.Value;
    }
    //modify or browse Current Record
    private void bindBaseData(string recordGID)
    {
        ConsignmentPartRecord model = Service.GetConsignmentPartRecordbykey(new ConsignmentPartRecord { RecordID = int.Parse(recordGID) });
        this.hidRecordID.Value = recordGID;
        this.txtDescription.Text = model.Description;
        if (model.Part != null)
        {
            this.hidCurrentPartID.Value = model.Part.PartID.ToString();
            this.labPartCode.Text = model.Part.PartCode;
            this.labPartName.Text = model.Part.PartChineseName;
            if (model.Part.Plant != null)
            {
                this.labPlantName.Text = model.Part.Plant.PlantCode;
            }
            if (model.Part.Supplier != null)
            {
                this.labDUNS.Text = model.Part.Supplier.DUNS;
            }
        }
        if (model.Supplier != null)
        {
            this.hidSupplierID.Value = model.Supplier.SupplierID.ToString();
            hidSupplierDUNS.Value = model.Supplier.DUNS; 
            this.txtSupplier.Text = model.Supplier.DUNS;
            this.txtFax.Text = model.Supplier.Fax;
            this.txtTelephone.Text = model.Supplier.PhoneNumber1;
        }
    }


    protected void bindCurrentPart()
    {
        if (this.hidCurrentPartID.Value.Length > 0)
        {
            ViewPart vp = Service.GetViewPartByKey(this.hidCurrentPartID.Value);
            this.labPartCode.Text = vp.PartCode;
            this.labPartName.Text = vp.PartChineseName;
            this.labPlantName.Text = vp.PlantName;
            this.labDUNS.Text = vp.DUNS;
        }
        else
        {
            Response.Write("<script>alert('请选择零件');</script>");
        }
    }

    protected void Toolbar1_ButtonClicked(object sender, SCS.Web.UI.WebControls.ButtonEventArgs e)
    {
        switch (e.CommandName)
        {
            case "save":
                Save();
                break;
            case "return":
                Response.Write("<script>window.location.href='ConsignmentPartRecordQuery.aspx';</script>");
                break;
            default:
                break;
        }
    }

    protected void Save()
    {
        bindCurrentPart();
        if (this.hidCurrentPartID.Value.Length > 0)
        {
            if (this.hidSupplierID.Value.Length > 0)
            {
                ConsignmentPartRecord model = new ConsignmentPartRecord();
                model.Part = new Part();
                model.Part.PartID = int.Parse(this.hidCurrentPartID.Value);
                model.Supplier = new Supplier();
                model.Supplier.SupplierID = int.Parse(this.hidSupplierID.Value);
                model.Description = this.txtDescription.Text.Trim();
                model.DateModified = DateTime.Now;

                if (hidRecordID.Value.Length > 0)
                {
                    model.RecordID = int.Parse(this.hidRecordID.Value);
                    Service.UpdateConsignmentPartRecord(model);
                }
                else
                {
                    model = Service.AddConsignmentPartRecord(model);
                    this.hidRecordID.Value = model.RecordID.ToString();
                }
            }
            else
            {
                Response.Write("<script>alert('请选择外协供应商DUNS');</script>");
                return;
            }
        }
    }
    
}
