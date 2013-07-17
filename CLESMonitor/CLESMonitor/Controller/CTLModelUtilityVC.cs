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
        /// <summary>The CTLModel that this utility-viewcontroller supports</summary>
        private CTLModel ctlModel;
        /// <summary>The XMLParser used by the CTLModel, enabling setting the filepath</summary>
        private XMLParser parser;
        /// <summary>A snapshot from CTLModel.activeEvents, used for building the ListView</summary>
        private List<CTLEvent> cachedActiveEvents;
        /// <summary>A snapshot from CTLModel.activeTasks, used for building the ListView</summary>
        private List<CTLTask> cachedActiveTasks;

        private List<CTLEvent> eventsToDisplay;
        private List<CTLTask> activeTasksToDisplay;
        private List<CTLTask> historyTasksToDisplay;
        private Timer listViewUpdateTimer;

        /// <summary>
        /// The constructor method.
        /// </summary>
        /// <param name="ctlModel">A instance of the CTLModel class</param>
        /// <param name="parser">A instance of the XMLParser class</param>
        public CTLModelUtilityVC(CTLModel ctlModel, XMLParser parser)
        {
            this.View = new CTLModelUtilityView();
            this.View.CTLModelUtilityViewShownHandler += new CTLModelUtilityView.EventHandler(viewControllerIsShown);
            this.View.openScenarioFileButtonClickedHandler += new CTLModelUtilityView.EventHandler(openScenarioFileDialog);
            this.View.clearListButtonClickedHandler += new CTLModelUtilityView.EventHandler(clearList);

            this.ctlModel = ctlModel;
            this.parser = parser;
            this.cachedActiveEvents = new List<CTLEvent>();
            this.cachedActiveTasks = new List<CTLTask>();
            this.eventsToDisplay = new List<CTLEvent>();
            this.activeTasksToDisplay = new List<CTLTask>();
            this.historyTasksToDisplay = new List<CTLTask>();
        }

        /// <summary>
        /// Called when the view has appeared for the first time.
        /// </summary>
        private void viewControllerIsShown()
        {
            // Timer to update the listView with data
            TimerCallback timerCallback = listViewUpdateTimerCallback;
            listViewUpdateTimer = new Timer(timerCallback, null, 0, 1000);
        }

        private void listViewUpdateTimerCallback(Object stateInfo)
        {
            bool updateUIRequired = false;

            // Compare the active events in the model to our own cached version
            IEnumerable<CTLEvent> eventsUnion = cachedActiveEvents.Union(ctlModel.activeEvents);
            IEnumerable<CTLEvent> eventsIntersect = cachedActiveEvents.Intersect(ctlModel.activeEvents);
            foreach (CTLEvent ctlEvent in eventsUnion.Except(eventsIntersect))
            {
                updateUIRequired = true;

                // A event has just become active
                if (ctlModel.activeEvents.Contains(ctlEvent))
                {
                    Console.WriteLine(ctlEvent.ToString());
                    eventsToDisplay.Insert(0, ctlEvent);
                }
            }
            cachedActiveEvents = new List<CTLEvent>(ctlModel.activeEvents); // FIXME: mogelijk niet thread-safe

            // Compare the active tasks in the model to our own cached version
            IEnumerable<CTLTask> tasksUnion = cachedActiveTasks.Union(ctlModel.activeTasks);
            IEnumerable<CTLTask> tasksIntersect = cachedActiveTasks.Intersect(ctlModel.activeTasks);
            foreach (CTLTask task in tasksUnion.Except(tasksIntersect))
            {
                // A task has just become active
                if (ctlModel.activeTasks.Contains(task))
                {
                    activeTasksToDisplay.Insert(0, task);
                    updateUIRequired = true;
                }
                // A task is no longer active
                else if (cachedActiveTasks.Contains(task))
                {
                    activeTasksToDisplay.Remove(task);
                    historyTasksToDisplay.Insert(0, task);
                    updateUIRequired = true;
                }
            }
            cachedActiveTasks = new List<CTLTask>(ctlModel.activeTasks); // FIXME: mogelijk niet thread-safe

            if (updateUIRequired)
            {
                updateUI();
            }
        }

        /// <summary>
        /// Updates the listView using the activecItems and historyItems
        /// </summary>
        public void updateUI()
        {
            IEnumerable<ListViewItem> listViewItems = new List<ListViewItem>();
            listViewItems = listViewItems.Union(generateEventsListViewItems());
            listViewItems = listViewItems.Union(generateActiveTasksListViewItems());
            listViewItems = listViewItems.Union(generateHistoryTasksListViewItems());

            try
            {
                View.Invoke((Action)(() =>
                {
                    try
                    {
                        // Delete all items that may be present
                        View.activeListView.Items.Clear();

                        // Add the generated active and history items
                        foreach (ListViewItem item in listViewItems)
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

        public List<ListViewItem> generateEventsListViewItems()
        {
            List<ListViewItem> eventsListViewItems = new List<ListViewItem>();

            foreach (CTLEvent ctlEvent in eventsToDisplay)
            {
                ListViewItem item = new ListViewItem(ctlEvent.identifier);

                item.SubItems.Add(ctlEvent.name);
                String timeSpanFormat = @"%h\:mm\:ss";
                item.SubItems.Add(ctlEvent.startTime.ToString(timeSpanFormat));
                string subItem = ctlEvent.inProgress ? "" : ctlEvent.endTime.ToString(timeSpanFormat);
                item.SubItems.Add(subItem);
                item.Group = View.activeListView.Groups["listViewGroup3"];

                eventsListViewItems.Add(item);
            }

            return eventsListViewItems;
        }

        public List<ListViewItem> generateActiveTasksListViewItems()
        {
            List<ListViewItem> activeTasksListViewItems = new List<ListViewItem>();
            foreach (CTLTask task in activeTasksToDisplay)
            {
                ListViewItem item = new ListViewItem("");

                item.SubItems.Add(task.name);
                String timeSpanFormat = @"%h\:mm\:ss";
                item.SubItems.Add(task.startTime.ToString(timeSpanFormat));
                item.SubItems.Add("");
                item.SubItems.Add("Deel van gebeurtenis ID: " + task.ctlEvent.identifier);
                item.Group = View.activeListView.Groups["listViewGroup1"];

                activeTasksListViewItems.Add(item);
            }
            return activeTasksListViewItems;
        }

        public List<ListViewItem> generateHistoryTasksListViewItems()
        {
            List<ListViewItem> historyItems = new List<ListViewItem>();
            foreach (CTLTask task in historyTasksToDisplay)
            {
                ListViewItem item = new ListViewItem("");

                item.SubItems.Add(task.name);
                String timeSpanFormat = @"%h\:mm\:ss";
                item.SubItems.Add(task.startTime.ToString(timeSpanFormat));
                item.SubItems.Add(task.endTime.ToString(timeSpanFormat));
                item.SubItems.Add("Deel van gebeurtenis ID: " + task.ctlEvent.identifier);
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

        /// <summary>
        /// Action method when the clear list button is clicked.
        /// </summary>
        public void clearList()
        {
            View.activeListView.Items.Clear();
            // Add a item without text to ensure the groupnames remain visible.
            foreach (ListViewGroup group in View.activeListView.Groups)
            {
                if (group.Items.Count == 0)
                {
                    View.activeListView.Items.Add(new ListViewItem(group));
                }
            }
            cachedActiveEvents.Clear();
            cachedActiveTasks.Clear();
            eventsToDisplay.Clear();
            activeTasksToDisplay.Clear();
            historyTasksToDisplay.Clear();
        }
    }
}
