using CLESMonitor.Model.CL;
using CLESMonitor.View;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using Timer = System.Threading.Timer;

namespace CLESMonitor.Controller
{
    /// <summary>
    /// The utility viewcontroller for CTLModel. Note that this viewcontrollers requires that
    /// the CTLInputSource of the CTLModel is implemented by a XMLParser.
    /// </summary>
    public class CTLModelUtilityVC
    {
        /// <summary>The view this viewcontroller manages</summary>
        public CTLModelUtilityView View { get; private set; }

        /// <summary>The CTLModel that this utility-viewcontroller interacts with</summary>
        private CTLModel ctlModel;
        /// <summary>A snapshot from CTLModel.activeTasks, used to build the ListView</summary>
        private List<CTLTask> cachedActiveTasks;
        private List<CTLTask> activeTasksToDisplay;
        private List<CTLTask> historyTasksToDisplay;

        /// <summary>The XMLParser (only CTLInputSource is available from CTLModel) to set the filepath</summary>
        private XMLParser parser;

        private Timer listViewUpdateTimer;

        /// <summary>
        /// The constructor method.
        /// </summary>
        /// <param name="ctlModel">A instance of the CTLModel class</param>
        /// <param name="parser">A instance of the XMLParser class</param>
        public CTLModelUtilityVC(CTLModel ctlModel, XMLParser parser)
        {
            this.View = new CTLModelUtilityView();
            this.View.openScenarioFileButtonClickedHandler += new CTLModelUtilityView.EventHandler(openScenarioFileDialog);
            this.View.CTLModelUtilityViewShownHandler += new CTLModelUtilityView.EventHandler(viewControllerIsShown);
            this.View.clearListButtonClickedHandler += new CTLModelUtilityView.EventHandler(clearList);

            this.ctlModel = ctlModel;
            this.parser = parser;
            this.cachedActiveTasks = new List<CTLTask>();
            this.activeTasksToDisplay = new List<CTLTask>();
            this.historyTasksToDisplay = new List<CTLTask>();
        }

        /// <summary>
        /// This method is called when the view has appeared for the first time.
        /// </summary>
        public void viewControllerIsShown()
        {
            // Timer to update the listView with active tasks
            TimerCallback timerCallback = listViewUpdateTimerCallback;
            listViewUpdateTimer = new Timer(timerCallback, null, 0, 1000);
        }

        private void listViewUpdateTimerCallback(Object stateInfo)
        {
            // Compare the active tasks in the model to our own cached version
            IEnumerable<CTLTask> tasksUnion = cachedActiveTasks.Union(ctlModel.activeTasks);
            IEnumerable<CTLTask> tasksIntersect = cachedActiveTasks.Intersect(ctlModel.activeTasks);
            bool displayTasksHaveChanged = false;
            foreach (CTLTask task in tasksUnion.Except(tasksIntersect))
            {
                // A task has just become active
                if (ctlModel.activeTasks.Contains(task))
                {
                    activeTasksToDisplay.Insert(0, task);
                    displayTasksHaveChanged = true;
                }
                // A task is no longer active
                else if (cachedActiveTasks.Contains(task))
                {
                    activeTasksToDisplay.Remove(task);
                    historyTasksToDisplay.Insert(0, task);
                    displayTasksHaveChanged = true;
                }
            }
            cachedActiveTasks = new List<CTLTask>(ctlModel.activeTasks); // FIXME: mogelijk niet thread-safe

            //TODO: er wordt niet altijd een eventnaam geprint
            if (displayTasksHaveChanged)
            {
                // Generate the listView items for the active group
                List<ListViewItem> activeItems = generateActiveItemList();

                // Generate the listView items for the history group
                List<ListViewItem> historyItems = generateHistoryList();

                // Update the UI
                updateUI(activeItems, historyItems);
            }
        }

        /// <summary>
        /// Updates the listView using the activecItems and historyItems
        /// </summary>
        /// <param name="activeItems">A list of tasks that are currently active</param>
        /// <param name="historyItems">A list of tasks that have ended</param>
        public void updateUI(List<ListViewItem> activeItems, List<ListViewItem> historyItems)
        {
            try
            {
                View.Invoke((Action)(() =>
                {
                    try
                    {
                        // Delete all items that may be present
                        View.activeListView.Items.Clear();

                        // Add the generated active and history items
                        foreach (ListViewItem item in activeItems.Union(historyItems))
                        {
                            View.activeListView.Items.Add(item);
                        }
                        // If no item is present in a group add a bogus item without text to ensure the groupnames remain visible.
                        foreach (ListViewGroup group in View.activeListView.Groups)
                        {
                            if (group.Items.Count == 0)
                            {
                                View.activeListView.Items.Add(new ListViewItem(group));
                            }
                        }
                    }
                    catch (ObjectDisposedException exception) { Console.WriteLine(exception.ToString()); }
                }));
            }
            catch (ObjectDisposedException exception) { Console.WriteLine(exception.ToString()); }
        }


        public List<ListViewItem> generateActiveItemList()
        {
            List<ListViewItem> activeItems = new List<ListViewItem>();
            foreach (CTLTask task in activeTasksToDisplay)
            {
                ListViewItem item = new ListViewItem(task.name);
                String timeSpanFormat = @"%h\:mm\:ss";

                item.SubItems.Add(task.startTime.ToString(timeSpanFormat));
                item.SubItems.Add("");

                foreach (CTLEvent ctlEvent in ctlModel.activeEvents)
                {
                    if (ctlEvent.identifier == task.eventIdentifier)
                    {
                        item.SubItems.Add(ctlEvent.name);
                    }
                }
                item.Group = View.activeListView.Groups["listViewGroup1"];

                activeItems.Add(item);
            }
            return activeItems;
        }

        public List<ListViewItem> generateHistoryList()
        {
            List<ListViewItem> historyItems = new List<ListViewItem>();
            foreach (CTLTask task in historyTasksToDisplay)
            {
                ListViewItem item = new ListViewItem(task.name);
                String timeSpanFormat = @"%h\:mm\:ss";

                item.SubItems.Add(task.startTime.ToString(timeSpanFormat));
                item.SubItems.Add(task.endTime.ToString(timeSpanFormat));

                foreach (CTLEvent ctlEvent in ctlModel.activeEvents)
                {
                    if (ctlEvent.identifier == task.eventIdentifier)
                    {
                        item.SubItems.Add(ctlEvent.name);
                    }
                }
                item.Group = View.activeListView.Groups["listViewGroup2"];

                historyItems.Add(item);
            }

            return historyItems;
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
                View.openScenarioFileButton.Text = "Verander Scenario";
                Console.WriteLine("Gekozen Scenario: " + View.openFileDialog.FileName);
            }
        }

        public void clearList()
        {
            View.activeListView.Items.Clear();
            // Add a bogus item without text to ensure the groupnames remain visible.
            foreach (ListViewGroup group in View.activeListView.Groups)
            {
                if (group.Items.Count == 0)
                {
                    View.activeListView.Items.Add(new ListViewItem(group));
                }
            }
            cachedActiveTasks.Clear();
            activeTasksToDisplay.Clear();
            historyTasksToDisplay.Clear();
        }
    }
}
