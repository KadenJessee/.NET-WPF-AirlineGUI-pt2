using Assignment6Flight;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Assignment6AirlineReservation
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        /// class that holds window for adding passenger
        /// </summary>
        wndAddPassenger wndAddPassenger;

        /// <summary>
        /// class for flightManager
        /// </summary>
        clsFlightManager flightManager;

        /// <summary>
        /// class for passengerManager
        /// </summary>
        clsPassengerManager passengerManager;

        /// <summary>
        /// determines if user click saved to add a passenger
        /// </summary>
        bool bAddPassengerMode;
        /// <summary>
        /// determines if user clicked change seat for updating a passengers seat
        /// </summary>
        bool bChangeSeatMode;

        /// <summary>
        /// instantiates the windoow and needed objects
        /// </summary>
        public MainWindow()
        {
            try
            {
                InitializeComponent();
                Application.Current.ShutdownMode = ShutdownMode.OnMainWindowClose;

                //set the object
                wndAddPassenger = new wndAddPassenger();
                //set the managers
                flightManager = new clsFlightManager();
                passengerManager = new clsPassengerManager();
                cbChooseFlight.ItemsSource = flightManager.GetFlights();
            }
            catch (Exception ex)
            {
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                    MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }

        /// <summary>
        /// selects flight, throws exception if flight/passenger are selected
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbChooseFlight_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cbChooseFlight.SelectedItem != null && cbChoosePassenger.SelectedItem != null)
            {
                throw new Exception("A flight and passenger have already been selected");
            }
            try
            {
                clsFlight clsSelectedFlight = (clsFlight)cbChooseFlight.SelectedItem;

                int id = clsSelectedFlight.sFlightID;

                //Flight ID = 1 display flight, else
                if (id == 1)
                {
                    CanvasA380.Visibility = Visibility.Visible;
                    Canvas767.Visibility = Visibility.Hidden;
                }
                else
                {
                    Canvas767.Visibility = Visibility.Visible;
                    CanvasA380.Visibility = Visibility.Hidden;
                }

                //enable the proper information
                cbChoosePassenger.IsEnabled = true;
                gPassengerCommands.IsEnabled = true;
                cmdAddPassenger.IsEnabled = true;
                //fill the combo box
                cbChoosePassenger.ItemsSource = passengerManager.GetPassengers(clsSelectedFlight.sFlightID.ToString());
                //create a list of passengers
                List<clsPassenger> passengers = passengerManager.GetPassengers(clsSelectedFlight.sFlightID.ToString());
            
                //FillPassengerSeats()
                FillPassengerSeats(id);

            }
            catch (Exception ex)
            {
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                    MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }

        /// <summary>
        /// fills the passengers seats with the needed color labels
        /// </summary>
        /// <param name="flightID"></param>
        public void FillPassengerSeats(int flightID)
        {
            try
            {
                if (flightID == 1)
                {
                    //reset all seats in the selected flight to blue
                    foreach (var child in cA380_Seats.Children)
                    {
                        if (child is Label label)
                        {
                            label.Background = Brushes.Blue;
                        }
                    }
                }
                else
                {
                    foreach (var child in c767_Seats.Children)
                    {
                        if (child is Label label)
                        {
                            label.Background = Brushes.Blue;
                        }
                    }
                }
                //loop through all passengers in the list
                //then loop through each seat in the selected flight, like "c767_Seats.Children"
                //Then compare the passengers seat to the label's content and if they match, change background to red since it's taken
                List<clsPassenger> passengers = passengerManager.GetPassengers(flightID.ToString());

                foreach(clsPassenger passenger in passengers)
                {
                    foreach(var child in (flightID == 1 ? cA380_Seats.Children : c767_Seats.Children))
                    {
                        if(child is Label label && label.Content.ToString() == passenger.SeatNum)
                        {
                            label.Background = Brushes.Red;
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                    MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }

        /// <summary>
        /// Opens up the AddPassenger window
        /// and disables functionality if saved
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdAddPassenger_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //hide this window
                this.Hide();
                //show the dialog
                wndAddPassenger.txtFirstName.Text = "";
                wndAddPassenger.txtLastName.Text = "";
                //make sure nothing is clicked on the screen
                clsFlight clsSelectedFlight = (clsFlight)cbChooseFlight.SelectedItem;

                int flightID = clsSelectedFlight.sFlightID;
                FillPassengerSeats(flightID);
                wndAddPassenger.ShowDialog();
                //show this
                this.Show();
                //Check the add passenger window to see if the user clicked save and if they did, then
                //disable everything except the seats, so they are forced to click a seat
                if(wndAddPassenger.boolSaved == true)
                {
                    gbPassengerInformation.IsEnabled = false;
                    gPassengerCommands.IsEnabled = false;
                    //set add passenger mode
                    bAddPassengerMode = true;
                    lblSelectSeat.Content = "Please select a seat";
                }
            }
            catch (Exception ex)
            {
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                    MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }

        /// <summary>
        /// for error handling
        /// </summary>
        /// <param name="sClass"></param>
        /// <param name="sMethod"></param>
        /// <param name="sMessage"></param>
        private void HandleError(string sClass, string sMethod, string sMessage)
        {
            try
            {
                MessageBox.Show(sClass + "." + sMethod + " -> " + sMessage);
            }
            catch (System.Exception ex)
            {
                System.IO.File.AppendAllText(@"C:\Error.txt", Environment.NewLine + "HandleError Exception: " + ex.Message);
            }
        }

        /// <summary>
        /// for when the change seat button is clicked
        /// sets the chanageseatmode to true and disables other functionality
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdChangeSeat_Click(object sender, RoutedEventArgs e)
        {
            if(cbChoosePassenger.SelectedItem == null)
            {
                throw new Exception("Need to select a passenger");
            }
            try
            {
                //Passenger is selected
                //Lock down window and set bChangeSeatMode, force user to select a seat
                if (cbChoosePassenger.SelectedItem != null)
                {
                    //update bChangeSeatMode
                    bChangeSeatMode = true;
                    //lock down other functions
                    gbPassengerInformation.IsEnabled = false;
                    gPassengerCommands.IsEnabled = false;
                }
            }
            catch (Exception ex)
            {
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                    MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }

        /// <summary>
        /// to help keep track of the keeping one seat green at a time
        /// </summary>
        private Label selectedSeat = null;

        /// <summary>
        /// has 3 different options of seat click, whether adding
        /// a passenger, changing the seat, or regular seat selection
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Seat_Click(object sender, MouseButtonEventArgs e)
        {
            try
            {
                Label MyLabel = (Label)sender;
                string sSeatNumber;
                clsPassenger Passenger;

                clsFlight clsSelectedFlight = (clsFlight)cbChooseFlight.SelectedItem;

                int flightID = clsSelectedFlight.sFlightID;

                if (selectedSeat != null && selectedSeat.Background == Brushes.Green)
                {
                    selectedSeat.Background = Brushes.Red;
                }

                //bAddPassengerMode
                if (bAddPassengerMode == true)
                {
                    if(MyLabel.Background == Brushes.Red)
                    {
                        throw new Exception("Seat is already taken, can't insert a passenger.");
                    }
                    if (MyLabel.Background == Brushes.Blue)
                    {
                        int passengerID = 0;
                        //Insert a new passenger into the database, then isnert a record into the link table (done in another class)
                        passengerManager.InsertPassenger(wndAddPassenger.txtFirstName.Text, wndAddPassenger.txtLastName.Text);
                        passengerID = passengerManager.GetPassengerID(wndAddPassenger.txtFirstName.Text, wndAddPassenger.txtLastName.Text);
                        //addPassenger()
                        passengerManager.InsertLinkTable(flightID, passengerID, MyLabel.Content.ToString());
                        //enable the controls
                        gbPassengerInformation.IsEnabled = true;
                        gPassengerCommands.IsEnabled = true;
                        bAddPassengerMode = false;

                        //load the passengers again
                        //fill the combo box
                        cbChoosePassenger.ItemsSource = passengerManager.GetPassengers(clsSelectedFlight.sFlightID.ToString());
                        MyLabel.Background = Brushes.Red;
                        lblSelectSeat.Content = "";
                    }
                    
                }
                //bChangeSeatMode
                //only change the seat if the seat is empty (blue)
                //if it's empty then update the link table to update the user's new seat (done in another class)
                else if (bChangeSeatMode == true)
                {
                    if(MyLabel.Background == Brushes.Red)
                    {
                        throw new Exception("Seat is already taken, cannot change seat to this location");
                    }
                    if (MyLabel.Background == Brushes.Blue)
                    {
                        Label previousLabel = null;
                        int passengerID = 0;
                        //get the flight id
                        clsPassenger passenger = (clsPassenger)cbChoosePassenger.SelectedItem;

                        //string seatTemp = passenger.SeatNum;
                        if (passenger != null)
                        {
                            passengerID = passengerManager.GetPassengerID(passenger.FirstName, passenger.LastName);
                            string previousSeat = passenger.SeatNum;
                            passengerManager.UpdatePassengerSeat(flightID, passengerID, MyLabel.Content.ToString());

                            MyLabel.Background = Brushes.Red;

                            //store current seat as previous seat
                            previousSeatLabel = MyLabel;
                            //enable features
                            gbPassengerInformation.IsEnabled = true;
                            gPassengerCommands.IsEnabled = true;

                        }

                        bChangeSeatMode = false;
                        //fill the combo box
                        cbChoosePassenger.ItemsSource = passengerManager.GetPassengers(clsSelectedFlight.sFlightID.ToString());
                        FillPassengerSeats(flightID);
                        
                    }
                }

                //otherwise in regular seat selection
                //if seat is taken (red), then loop through the passengers in the combo box,
                //and keep looping until the seat that was clicked, its number matches a passenger's seat number,
                //then select that combo box index or selected item and put the passenger's seat in the label
                if (MyLabel.Background == Brushes.Red)
                {
                    FillPassengerSeats(flightID);
                    //turn the seat green
                    MyLabel.Background = Brushes.Green;

                    //get the seat number
                    sSeatNumber = MyLabel.Content.ToString();

                    //loop through the items in the combo box
                    for (int i = 0; i < cbChoosePassenger.Items.Count; i++)
                    {
                        //extract the passenger from the combo box
                        Passenger = (clsPassenger)cbChoosePassenger.Items[i];

                        //if seat num matches select passenger from combo box
                        if (sSeatNumber == Passenger.SeatNum)
                        {
                            cbChoosePassenger.SelectedIndex = i;
                            lblPassengersSeatNumber.Content = sSeatNumber;
                        }
                    }
                    selectedSeat = MyLabel;
                    
                }


                //if clicking an emtpy seat, don't choose a passenger, set passenger's seat to nothing
                if (MyLabel.Background == Brushes.Blue && bAddPassengerMode != true && bChangeSeatMode != true)
                {
                    cbChoosePassenger.SelectedItem = null;
                    lblPassengersSeatNumber.Content = "";
                }
            }
            catch (Exception ex)
            {
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                    MethodInfo.GetCurrentMethod().Name, ex.Message);
            }

        }

        /// <summary>
        /// deletes the passenger from the database and the WPF form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdDeletePassenger_Click(object sender, RoutedEventArgs e)
        {
            if (cbChoosePassenger.SelectedItem == null)
            {
                throw new Exception("Need to select a passenger");
            }
            try
            {
                if (cbChoosePassenger.SelectedItem != null)
                {
                    //get the passenger name
                    clsPassenger selectedPassenger = (clsPassenger)cbChoosePassenger.SelectedItem;
                    string previousSeat = selectedPassenger.SeatNum;

                    int passengerID = 0;
                    //get the id
                    passengerID = passengerManager.GetPassengerID(selectedPassenger.FirstName, selectedPassenger.LastName);


                    //get the flight id
                    clsFlight clsSelectedFlight = (clsFlight)cbChooseFlight.SelectedItem;
                    int flightID = clsSelectedFlight.sFlightID;
                    //delete the link table
                    passengerManager.DeletePassengerLink(flightID, passengerID);
                    //delete the passenger
                    passengerManager.DeletePassenger(passengerID);


                    //reload the passengers into combo box
                    //fill the combo box
                    cbChoosePassenger.ItemsSource = passengerManager.GetPassengers(clsSelectedFlight.sFlightID.ToString());
                    //reload the seats
                    FillPassengerSeats(flightID);

                    //clear that selected seat label
                    lblPassengersSeatNumber.Content = "";

                    //clear the previous label
                    previousSeatLabel = null;

                }
            }
            catch (Exception ex)
            {
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                    MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }

        /// <summary>
        /// used to keep track of previous labels to turn them red
        /// </summary>
        private Label previousSeatLabel = null;

        /// <summary>
        /// displays the users seat number and changes their seat to green insead of red
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbChoosePassenger_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                //create passenger object
                clsPassenger Passenger;

                //get the selected passenger
                Passenger = (clsPassenger)cbChoosePassenger.SelectedItem;

                //set the seat label
                if (Passenger != null)
                {
                    lblPassengersSeatNumber.Content = Passenger.SeatNum.ToString();
                }
                else
                {
                    lblPassengersSeatNumber.Content = "";
                }

                //get the selected flight
                clsFlight clsSelectedFlight = (clsFlight)cbChooseFlight.SelectedItem;
                //get flight id for the canvas
                int flightID = clsSelectedFlight.sFlightID;

                Label newSelectedSeatLabel = null;
                //need to find thee selected seat in panel. Loop through each label in the panel
                foreach (Label MyLabel in (flightID == 1 ? cA380_Seats.Children : c767_Seats.Children))
                {
                    //make sure passenger isn't null (for debug) and that the content is same as seatNum
                    if (Passenger != null && MyLabel.Content.ToString() == Passenger.SeatNum.ToString())
                    {
                        MyLabel.Background = Brushes.Green;

                        //change the color of the previous back to red
                        if (previousSeatLabel != null && previousSeatLabel != MyLabel)
                        {
                            previousSeatLabel.Background = Brushes.Red;
                        }

                        //store current seat as previous seat
                        previousSeatLabel = MyLabel;
                    }
                }
            }
            catch (Exception ex)
            {
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                    MethodInfo.GetCurrentMethod().Name, ex.Message);
            }

        }
    }
}
