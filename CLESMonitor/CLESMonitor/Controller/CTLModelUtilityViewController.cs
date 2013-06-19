using CLESMonitor.Model.CL;
using CLESMonitor.View;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Timer = System.Threading.Timer;

namespace CLESMonitor.Controller
{
    /// <summary>
    /// The utility viewcontroller for CTLModel. Note that this viewcontrollers requires that
    /// the CTLInputSource of the CTLModel is implemented by a XMLParser.
    /// </summary>
    public class CTLModelUtilityViewController
    {
        /// <summary>The view this viewcontroller manages</summary>
        public CTLModelUtilityView View { get; private set; }
        /// <summary>The CTLModel that this utility-viewcontroller interacts with</summary>
        private CTLModel ctlModel;
        /// <summary>The XMLParser (only CTLInputSource is available from CTLModel) to set the filepath</summary>
        private XMLParser parser;

        /// <summary>
        /// The constructor method.
        /// </summary>
        /// <param name="ctlModel">A instance of the CTLModel class</param>
        /// <param name="parser">A instance of the XMLParser class</param>
        public CTLModelUtilityViewController(CTLModel ctlModel, XMLParser parser)
        {
            this.View = new CTLModelUtilityView(this);
            this.ctlModel = ctlModel;
            this.parser = parser;
        }

        /// <summary>
        /// This method is called when the view has appeared for the first time.
        /// </summary>
        public void viewControllerIsShown()
        {
            // Timer to update the listView with active tasks
            TimerCallback timerCallback = listViewUpdateTimerCallback;
            Timer timer = new Timer(timerCallback, null, 0, 1000);
        }

        private void listViewUpdateTimerCallback(Object stateInfo)
        {
            // Duplicate the list
            List<CTLTask> tasks = new List<CTLTask>(ctlModel.activeTasks);
            List<ListViewItem> items = new List<ListViewItem>();

            foreach (CTLTask ctlTask in tasks)
            {
                ListViewItem item = new ListViewItem(ctlTask.name);
                String timeSpanFormat = @"%h\:mm\:ss";
                item.SubItems.Add(ctlTask.startTime.ToString(timeSpanFormat));
                item.SubItems.Add(ctlTask.endTime.ToString(timeSpanFormat));
                item.Group = View.activeListView.Groups["listViewGroup1"];

                items.Add(item);
            }

            // Update the GUI
            View.activeListView.Invoke((Action)(() =>
            {
                List<ListViewItem> itemsForDeletion = new List<ListViewItem>();
                foreach (ListViewItem item in View.activeListView.Groups["listViewGroup1"].Items)
                {
                    itemsForDeletion.Add(item);
                }
                foreach (ListViewItem item in itemsForDeletion)
                {
                    View.activeListView.Items.Remove(item);
                }

                foreach (ListViewItem item in items)
                {
                    View.activeListView.Items.Add(item);
                }
            }));
        }

        /// <summary>
        /// Action method when the openScenarioFileButton is clicked.
        /// </summary>
        public void openScenarioFileDialog()
        {
            if (View.openFileDialog.ShowDialog() == DialogResult.OK)
            {
                StreamReader streamReader = new StreamReader(File.Open(View.openFileDialog.FileName, FileMode.Open));
                parser.loadTextReader(streamReader);
                Console.WriteLine("Gekozen file: " + View.openFileDialog.FileName);
            }
        }
    }
}
