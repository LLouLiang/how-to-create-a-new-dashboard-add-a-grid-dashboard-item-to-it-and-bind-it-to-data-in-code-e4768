using System;
using System.Windows.Forms;
using DevExpress.DashboardCommon;
using DevExpress.DataAccess;

namespace Dashboard_CreateGrid {
    public partial class Form1 : Form {
        public Form1() {
            InitializeComponent();
        }
        private GridDashboardItem CreateGrid(DataSource dataSource) {

            // Creates a grid dashboard item and specifies its data source.
            GridDashboardItem grid = new GridDashboardItem();
            grid.DataSource = dataSource;

            // Creates new grid columns of the specified type and with the specified dimension or
            // measure. Then, adds these columns to the grid's Columns collection.
            grid.Columns.Add(new GridDimensionColumn(new Dimension("CategoryName")));            
            grid.Columns.Add(new GridMeasureColumn(new Measure("Extended Price")));
            grid.Columns.Add(new GridDeltaColumn(new Measure("Extended Price", SummaryType.Max), 
                                                 new Measure("Extended Price", SummaryType.Min)));
            grid.Columns.Add(new GridSparklineColumn(new Measure("Extended Price")));
            grid.SparklineArgument = new Dimension("OrderDate", DateTimeGroupInterval.MonthYear);

            return grid;
        }
        private void Form1_Load(object sender, EventArgs e) {

            // Creates a dashboard and sets it as a currently opened dashboard in the dashboard viewer.
            dashboardViewer1.Dashboard = new Dashboard();

            // Creates a data source and adds it to the dashboard data source collection.
            DataSource dataSource = new DataSource("Sales Person");
            dashboardViewer1.Dashboard.DataSources.Add(dataSource);

            // Creates a grid dashboard item with the specified data source 
            // and adds it to the Items collection to display within the dashboard.
            GridDashboardItem grid = CreateGrid(dataSource);
            dashboardViewer1.Dashboard.Items.Add(grid);

            // Reloads data in the data sources.
            dashboardViewer1.ReloadData();
        }
        private void dashboardViewer1_DataLoading(object sender, DataLoadingEventArgs e) {

            // Specifies data for the current data source.
            e.Data = (new nwindDataSetTableAdapters.SalesPersonTableAdapter()).GetData();
        }
    }
}
