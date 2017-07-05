namespace Towzin.Report
{
    partial class OrderStatus
    {
        #region Component Designer generated code
        /// <summary>
        /// Required method for telerik Reporting designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            Telerik.Reporting.Group group1 = new Telerik.Reporting.Group();
            Telerik.Reporting.Group group2 = new Telerik.Reporting.Group();
            Telerik.Reporting.Drawing.StyleRule styleRule1 = new Telerik.Reporting.Drawing.StyleRule();
            Telerik.Reporting.Drawing.StyleRule styleRule2 = new Telerik.Reporting.Drawing.StyleRule();
            Telerik.Reporting.Drawing.StyleRule styleRule3 = new Telerik.Reporting.Drawing.StyleRule();
            Telerik.Reporting.Drawing.StyleRule styleRule4 = new Telerik.Reporting.Drawing.StyleRule();
            Telerik.Reporting.Drawing.StyleRule styleRule5 = new Telerik.Reporting.Drawing.StyleRule();
            this.sqlDataSource1 = new Telerik.Reporting.SqlDataSource();
            this.labelsGroupHeaderSection = new Telerik.Reporting.GroupHeaderSection();
            this.labelsGroupFooterSection = new Telerik.Reporting.GroupFooterSection();
            this.wasteCaptionTextBox = new Telerik.Reporting.TextBox();
            this.expr1CaptionTextBox = new Telerik.Reporting.TextBox();
            this.productionLineNameCaptionTextBox = new Telerik.Reporting.TextBox();
            this.partCodeCaptionTextBox = new Telerik.Reporting.TextBox();
            this.amountCaptionTextBox = new Telerik.Reporting.TextBox();
            this.iOCaptionTextBox = new Telerik.Reporting.TextBox();
            this.wasteGroupHeaderSection = new Telerik.Reporting.GroupHeaderSection();
            this.wasteGroupFooterSection = new Telerik.Reporting.GroupFooterSection();
            this.textBox1 = new Telerik.Reporting.TextBox();
            this.pageHeader = new Telerik.Reporting.PageHeaderSection();
            this.reportNameTextBox = new Telerik.Reporting.TextBox();
            this.pageFooter = new Telerik.Reporting.PageFooterSection();
            this.currentTimeTextBox = new Telerik.Reporting.TextBox();
            this.pageInfoTextBox = new Telerik.Reporting.TextBox();
            this.reportHeader = new Telerik.Reporting.ReportHeaderSection();
            this.titleTextBox = new Telerik.Reporting.TextBox();
            this.orderCodeIDCaptionTextBox = new Telerik.Reporting.TextBox();
            this.orderCodeIDDataTextBox = new Telerik.Reporting.TextBox();
            this.reportFooter = new Telerik.Reporting.ReportFooterSection();
            this.detail = new Telerik.Reporting.DetailSection();
            this.expr1DataTextBox = new Telerik.Reporting.TextBox();
            this.productionLineNameDataTextBox = new Telerik.Reporting.TextBox();
            this.partCodeDataTextBox = new Telerik.Reporting.TextBox();
            this.amountDataTextBox = new Telerik.Reporting.TextBox();
            this.iODataTextBox = new Telerik.Reporting.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // sqlDataSource1
            // 
            this.sqlDataSource1.ConnectionString = "DefaultConnection";
            this.sqlDataSource1.Name = "sqlDataSource1";
            this.sqlDataSource1.SelectCommand = "SELECT        VW_ProductiveDetails_Index.*\r\nFROM            VW_ProductiveDetails_" +
    "Index";
            // 
            // labelsGroupHeaderSection
            // 
            this.labelsGroupHeaderSection.Height = Telerik.Reporting.Drawing.Unit.Inch(0.36892721056938171D);
            this.labelsGroupHeaderSection.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.wasteCaptionTextBox,
            this.expr1CaptionTextBox,
            this.productionLineNameCaptionTextBox,
            this.partCodeCaptionTextBox,
            this.amountCaptionTextBox,
            this.iOCaptionTextBox,
            this.orderCodeIDCaptionTextBox});
            this.labelsGroupHeaderSection.Name = "labelsGroupHeaderSection";
            this.labelsGroupHeaderSection.PrintOnEveryPage = true;
            // 
            // labelsGroupFooterSection
            // 
            this.labelsGroupFooterSection.Height = Telerik.Reporting.Drawing.Unit.Inch(0.28125D);
            this.labelsGroupFooterSection.Name = "labelsGroupFooterSection";
            this.labelsGroupFooterSection.Style.Visible = false;
            // 
            // wasteCaptionTextBox
            // 
            this.wasteCaptionTextBox.CanGrow = true;
            this.wasteCaptionTextBox.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(1.5D), Telerik.Reporting.Drawing.Unit.Inch(0.068927288055419922D));
            this.wasteCaptionTextBox.Name = "wasteCaptionTextBox";
            this.wasteCaptionTextBox.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.0520833730697632D), Telerik.Reporting.Drawing.Unit.Inch(0.20000000298023224D));
            this.wasteCaptionTextBox.StyleName = "Caption";
            this.wasteCaptionTextBox.Value = "Waste";
            // 
            // expr1CaptionTextBox
            // 
            this.expr1CaptionTextBox.CanGrow = true;
            this.expr1CaptionTextBox.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(2.5729167461395264D), Telerik.Reporting.Drawing.Unit.Inch(0.068927288055419922D));
            this.expr1CaptionTextBox.Name = "expr1CaptionTextBox";
            this.expr1CaptionTextBox.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.0520833730697632D), Telerik.Reporting.Drawing.Unit.Inch(0.20000000298023224D));
            this.expr1CaptionTextBox.StyleName = "Caption";
            this.expr1CaptionTextBox.Value = "Expr1";
            // 
            // productionLineNameCaptionTextBox
            // 
            this.productionLineNameCaptionTextBox.CanGrow = true;
            this.productionLineNameCaptionTextBox.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(3.6458332538604736D), Telerik.Reporting.Drawing.Unit.Inch(0.068927288055419922D));
            this.productionLineNameCaptionTextBox.Name = "productionLineNameCaptionTextBox";
            this.productionLineNameCaptionTextBox.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.0520833730697632D), Telerik.Reporting.Drawing.Unit.Inch(0.20000000298023224D));
            this.productionLineNameCaptionTextBox.StyleName = "Caption";
            this.productionLineNameCaptionTextBox.Value = "Production Line Name";
            // 
            // partCodeCaptionTextBox
            // 
            this.partCodeCaptionTextBox.CanGrow = true;
            this.partCodeCaptionTextBox.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(4.71875D), Telerik.Reporting.Drawing.Unit.Inch(0.068927288055419922D));
            this.partCodeCaptionTextBox.Name = "partCodeCaptionTextBox";
            this.partCodeCaptionTextBox.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.0520833730697632D), Telerik.Reporting.Drawing.Unit.Inch(0.20000000298023224D));
            this.partCodeCaptionTextBox.StyleName = "Caption";
            this.partCodeCaptionTextBox.Value = "Part Code";
            // 
            // amountCaptionTextBox
            // 
            this.amountCaptionTextBox.CanGrow = true;
            this.amountCaptionTextBox.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(5.7916665077209473D), Telerik.Reporting.Drawing.Unit.Inch(0.068927288055419922D));
            this.amountCaptionTextBox.Name = "amountCaptionTextBox";
            this.amountCaptionTextBox.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.0520833730697632D), Telerik.Reporting.Drawing.Unit.Inch(0.20000000298023224D));
            this.amountCaptionTextBox.StyleName = "Caption";
            this.amountCaptionTextBox.Value = "Amount";
            // 
            // iOCaptionTextBox
            // 
            this.iOCaptionTextBox.CanGrow = true;
            this.iOCaptionTextBox.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(6.8645834922790527D), Telerik.Reporting.Drawing.Unit.Inch(0.068927288055419922D));
            this.iOCaptionTextBox.Name = "iOCaptionTextBox";
            this.iOCaptionTextBox.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.0520833730697632D), Telerik.Reporting.Drawing.Unit.Inch(0.20000000298023224D));
            this.iOCaptionTextBox.StyleName = "Caption";
            this.iOCaptionTextBox.Value = "IO";
            // 
            // wasteGroupHeaderSection
            // 
            this.wasteGroupHeaderSection.Height = Telerik.Reporting.Drawing.Unit.Inch(0.28125D);
            this.wasteGroupHeaderSection.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.orderCodeIDDataTextBox});
            this.wasteGroupHeaderSection.Name = "wasteGroupHeaderSection";
            // 
            // wasteGroupFooterSection
            // 
            this.wasteGroupFooterSection.Height = Telerik.Reporting.Drawing.Unit.Inch(0.28125D);
            this.wasteGroupFooterSection.Name = "wasteGroupFooterSection";
            // 
            // textBox1
            // 
            this.textBox1.CanGrow = true;
            this.textBox1.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(1.5D), Telerik.Reporting.Drawing.Unit.Inch(0.020833492279052734D));
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.0520833730697632D), Telerik.Reporting.Drawing.Unit.Inch(0.20000000298023224D));
            this.textBox1.StyleName = "Data";
            this.textBox1.Value = "= Fields.Waste";
            // 
            // pageHeader
            // 
            this.pageHeader.Height = Telerik.Reporting.Drawing.Unit.Inch(0.28125D);
            this.pageHeader.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.reportNameTextBox});
            this.pageHeader.Name = "pageHeader";
            // 
            // reportNameTextBox
            // 
            this.reportNameTextBox.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0.02083333395421505D), Telerik.Reporting.Drawing.Unit.Inch(0.02083333395421505D));
            this.reportNameTextBox.Name = "reportNameTextBox";
            this.reportNameTextBox.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(6.4166665077209473D), Telerik.Reporting.Drawing.Unit.Inch(0.20000000298023224D));
            this.reportNameTextBox.StyleName = "PageInfo";
            this.reportNameTextBox.Value = "OrderStatus";
            // 
            // pageFooter
            // 
            this.pageFooter.Height = Telerik.Reporting.Drawing.Unit.Inch(0.28125D);
            this.pageFooter.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.currentTimeTextBox,
            this.pageInfoTextBox});
            this.pageFooter.Name = "pageFooter";
            // 
            // currentTimeTextBox
            // 
            this.currentTimeTextBox.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0.02083333395421505D), Telerik.Reporting.Drawing.Unit.Inch(0.02083333395421505D));
            this.currentTimeTextBox.Name = "currentTimeTextBox";
            this.currentTimeTextBox.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(3.1979167461395264D), Telerik.Reporting.Drawing.Unit.Inch(0.20000000298023224D));
            this.currentTimeTextBox.StyleName = "PageInfo";
            this.currentTimeTextBox.Value = "=NOW()";
            // 
            // pageInfoTextBox
            // 
            this.pageInfoTextBox.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(3.2395832538604736D), Telerik.Reporting.Drawing.Unit.Inch(0.02083333395421505D));
            this.pageInfoTextBox.Name = "pageInfoTextBox";
            this.pageInfoTextBox.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(3.1979167461395264D), Telerik.Reporting.Drawing.Unit.Inch(0.20000000298023224D));
            this.pageInfoTextBox.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.pageInfoTextBox.StyleName = "PageInfo";
            this.pageInfoTextBox.Value = "=PageNumber";
            // 
            // reportHeader
            // 
            this.reportHeader.Height = Telerik.Reporting.Drawing.Unit.Inch(1.0498228073120117D);
            this.reportHeader.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.titleTextBox});
            this.reportHeader.Name = "reportHeader";
            // 
            // titleTextBox
            // 
            this.titleTextBox.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0D), Telerik.Reporting.Drawing.Unit.Inch(0D));
            this.titleTextBox.Name = "titleTextBox";
            this.titleTextBox.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(8.0000009536743164D), Telerik.Reporting.Drawing.Unit.Inch(0.787401556968689D));
            this.titleTextBox.StyleName = "Title";
            this.titleTextBox.Value = "OrderStatus";
            // 
            // orderCodeIDCaptionTextBox
            // 
            this.orderCodeIDCaptionTextBox.CanGrow = true;
            this.orderCodeIDCaptionTextBox.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0.19999997317790985D), Telerik.Reporting.Drawing.Unit.Inch(0.068927288055419922D));
            this.orderCodeIDCaptionTextBox.Name = "orderCodeIDCaptionTextBox";
            this.orderCodeIDCaptionTextBox.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.0584121942520142D), Telerik.Reporting.Drawing.Unit.Inch(0.20000000298023224D));
            this.orderCodeIDCaptionTextBox.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.orderCodeIDCaptionTextBox.StyleName = "Caption";
            this.orderCodeIDCaptionTextBox.Value = "Order Code ID:";
            // 
            // orderCodeIDDataTextBox
            // 
            this.orderCodeIDDataTextBox.CanGrow = true;
            this.orderCodeIDDataTextBox.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0.04158782958984375D), Telerik.Reporting.Drawing.Unit.Inch(0.02083333395421505D));
            this.orderCodeIDDataTextBox.Name = "orderCodeIDDataTextBox";
            this.orderCodeIDDataTextBox.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.3604167699813843D), Telerik.Reporting.Drawing.Unit.Inch(0.20000000298023224D));
            this.orderCodeIDDataTextBox.StyleName = "Data";
            this.orderCodeIDDataTextBox.Value = "= Fields.OrderCodeID";
            // 
            // reportFooter
            // 
            this.reportFooter.Height = Telerik.Reporting.Drawing.Unit.Inch(0.28125D);
            this.reportFooter.Name = "reportFooter";
            // 
            // detail
            // 
            this.detail.Height = Telerik.Reporting.Drawing.Unit.Inch(0.28125D);
            this.detail.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.partCodeDataTextBox,
            this.amountDataTextBox,
            this.iODataTextBox,
            this.productionLineNameDataTextBox,
            this.textBox1,
            this.expr1DataTextBox});
            this.detail.Name = "detail";
            // 
            // expr1DataTextBox
            // 
            this.expr1DataTextBox.CanGrow = true;
            this.expr1DataTextBox.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(2.5729167461395264D), Telerik.Reporting.Drawing.Unit.Inch(0.02083333395421505D));
            this.expr1DataTextBox.Name = "expr1DataTextBox";
            this.expr1DataTextBox.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.0520833730697632D), Telerik.Reporting.Drawing.Unit.Inch(0.20000000298023224D));
            this.expr1DataTextBox.StyleName = "Data";
            this.expr1DataTextBox.Value = "= Fields.Expr1";
            // 
            // productionLineNameDataTextBox
            // 
            this.productionLineNameDataTextBox.CanGrow = true;
            this.productionLineNameDataTextBox.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(3.7000000476837158D), Telerik.Reporting.Drawing.Unit.Inch(0.02083333395421505D));
            this.productionLineNameDataTextBox.Name = "productionLineNameDataTextBox";
            this.productionLineNameDataTextBox.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.0520833730697632D), Telerik.Reporting.Drawing.Unit.Inch(0.20000000298023224D));
            this.productionLineNameDataTextBox.StyleName = "Data";
            this.productionLineNameDataTextBox.Value = "= Fields.ProductionLineName";
            // 
            // partCodeDataTextBox
            // 
            this.partCodeDataTextBox.CanGrow = true;
            this.partCodeDataTextBox.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(4.7521624565124512D), Telerik.Reporting.Drawing.Unit.Inch(0.02083333395421505D));
            this.partCodeDataTextBox.Name = "partCodeDataTextBox";
            this.partCodeDataTextBox.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.0520833730697632D), Telerik.Reporting.Drawing.Unit.Inch(0.20000000298023224D));
            this.partCodeDataTextBox.StyleName = "Data";
            this.partCodeDataTextBox.Value = "= Fields.PartCode";
            // 
            // amountDataTextBox
            // 
            this.amountDataTextBox.CanGrow = true;
            this.amountDataTextBox.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(5.82507848739624D), Telerik.Reporting.Drawing.Unit.Inch(0.02083333395421505D));
            this.amountDataTextBox.Name = "amountDataTextBox";
            this.amountDataTextBox.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.0520833730697632D), Telerik.Reporting.Drawing.Unit.Inch(0.20000000298023224D));
            this.amountDataTextBox.StyleName = "Data";
            this.amountDataTextBox.Value = "= Fields.Amount";
            // 
            // iODataTextBox
            // 
            this.iODataTextBox.CanGrow = true;
            this.iODataTextBox.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(6.8979926109313965D), Telerik.Reporting.Drawing.Unit.Inch(0.02083333395421505D));
            this.iODataTextBox.Name = "iODataTextBox";
            this.iODataTextBox.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.0520833730697632D), Telerik.Reporting.Drawing.Unit.Inch(0.20000000298023224D));
            this.iODataTextBox.StyleName = "Data";
            this.iODataTextBox.Value = "= Fields.IO";
            // 
            // OrderStatus
            // 
            this.DataSource = this.sqlDataSource1;
            group1.GroupFooter = this.labelsGroupFooterSection;
            group1.GroupHeader = this.labelsGroupHeaderSection;
            group1.Name = "labelsGroup";
            group2.GroupFooter = this.wasteGroupFooterSection;
            group2.GroupHeader = this.wasteGroupHeaderSection;
            group2.Groupings.Add(new Telerik.Reporting.Grouping("= Fields.Waste"));
            group2.Name = "wasteGroup";
            this.Groups.AddRange(new Telerik.Reporting.Group[] {
            group1,
            group2});
            this.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.labelsGroupHeaderSection,
            this.labelsGroupFooterSection,
            this.wasteGroupHeaderSection,
            this.wasteGroupFooterSection,
            this.pageHeader,
            this.pageFooter,
            this.reportHeader,
            this.reportFooter,
            this.detail});
            this.Name = "OrderStatus";
            this.PageSettings.Margins = new Telerik.Reporting.Drawing.MarginsU(Telerik.Reporting.Drawing.Unit.Inch(1D), Telerik.Reporting.Drawing.Unit.Inch(1D), Telerik.Reporting.Drawing.Unit.Inch(1D), Telerik.Reporting.Drawing.Unit.Inch(1D));
            this.PageSettings.PaperKind = System.Drawing.Printing.PaperKind.Letter;
            styleRule1.Selectors.AddRange(new Telerik.Reporting.Drawing.ISelector[] {
            new Telerik.Reporting.Drawing.TypeSelector(typeof(Telerik.Reporting.TextItemBase)),
            new Telerik.Reporting.Drawing.TypeSelector(typeof(Telerik.Reporting.HtmlTextBox))});
            styleRule1.Style.Padding.Left = Telerik.Reporting.Drawing.Unit.Point(2D);
            styleRule1.Style.Padding.Right = Telerik.Reporting.Drawing.Unit.Point(2D);
            styleRule2.Selectors.AddRange(new Telerik.Reporting.Drawing.ISelector[] {
            new Telerik.Reporting.Drawing.StyleSelector("Title")});
            styleRule2.Style.Color = System.Drawing.Color.Black;
            styleRule2.Style.Font.Bold = true;
            styleRule2.Style.Font.Name = "Tahoma";
            styleRule2.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(18D);
            styleRule3.Selectors.AddRange(new Telerik.Reporting.Drawing.ISelector[] {
            new Telerik.Reporting.Drawing.StyleSelector("Caption")});
            styleRule3.Style.Color = System.Drawing.Color.Black;
            styleRule3.Style.Font.Name = "Tahoma";
            styleRule3.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(10D);
            styleRule3.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            styleRule4.Selectors.AddRange(new Telerik.Reporting.Drawing.ISelector[] {
            new Telerik.Reporting.Drawing.StyleSelector("Data")});
            styleRule4.Style.Font.Name = "Tahoma";
            styleRule4.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(9D);
            styleRule4.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            styleRule5.Selectors.AddRange(new Telerik.Reporting.Drawing.ISelector[] {
            new Telerik.Reporting.Drawing.StyleSelector("PageInfo")});
            styleRule5.Style.Font.Name = "Tahoma";
            styleRule5.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8D);
            styleRule5.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.StyleSheet.AddRange(new Telerik.Reporting.Drawing.StyleRule[] {
            styleRule1,
            styleRule2,
            styleRule3,
            styleRule4,
            styleRule5});
            this.Width = Telerik.Reporting.Drawing.Unit.Inch(8.05007553100586D);
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }
        #endregion

        private Telerik.Reporting.SqlDataSource sqlDataSource1;
        private Telerik.Reporting.GroupHeaderSection labelsGroupHeaderSection;
        private Telerik.Reporting.TextBox wasteCaptionTextBox;
        private Telerik.Reporting.TextBox expr1CaptionTextBox;
        private Telerik.Reporting.TextBox productionLineNameCaptionTextBox;
        private Telerik.Reporting.TextBox partCodeCaptionTextBox;
        private Telerik.Reporting.TextBox amountCaptionTextBox;
        private Telerik.Reporting.TextBox iOCaptionTextBox;
        private Telerik.Reporting.GroupFooterSection labelsGroupFooterSection;
        private Telerik.Reporting.GroupHeaderSection wasteGroupHeaderSection;
        private Telerik.Reporting.TextBox textBox1;
        private Telerik.Reporting.GroupFooterSection wasteGroupFooterSection;
        private Telerik.Reporting.PageHeaderSection pageHeader;
        private Telerik.Reporting.TextBox reportNameTextBox;
        private Telerik.Reporting.PageFooterSection pageFooter;
        private Telerik.Reporting.TextBox currentTimeTextBox;
        private Telerik.Reporting.TextBox pageInfoTextBox;
        private Telerik.Reporting.ReportHeaderSection reportHeader;
        private Telerik.Reporting.TextBox titleTextBox;
        private Telerik.Reporting.TextBox orderCodeIDCaptionTextBox;
        private Telerik.Reporting.TextBox orderCodeIDDataTextBox;
        private Telerik.Reporting.ReportFooterSection reportFooter;
        private Telerik.Reporting.DetailSection detail;
        private Telerik.Reporting.TextBox expr1DataTextBox;
        private Telerik.Reporting.TextBox productionLineNameDataTextBox;
        private Telerik.Reporting.TextBox partCodeDataTextBox;
        private Telerik.Reporting.TextBox amountDataTextBox;
        private Telerik.Reporting.TextBox iODataTextBox;
    }
}