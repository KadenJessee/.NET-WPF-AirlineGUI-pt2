using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Assignment6Flight
{
    /// <summary>
    /// business logic for flight class
    /// </summary>
    internal class clsFlightManager
    {
        /// <summary>
        /// object to connect to the data access
        /// </summary>
        clsDataAccess db;

        /// <summary>
        /// creates object for the data access
        /// </summary>
        public clsFlightManager()
        {
            try
            {
                db = new clsDataAccess();
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }

        }

        /// <summary>
        /// gets the flights from SQL queries and returns a list of clsFlights
        /// </summary>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public List<clsFlight> GetFlights()
        {
            try
            {
                //create a new list
                List<clsFlight> flights = new List<clsFlight>();
                int i = 0;

                //get the sql statement
                string sSQL = clsSQL.GetFlights();

                //execute the statement
                DataSet dsFlights = new DataSet();

                dsFlights = db.ExecuteSQLStatement(sSQL, ref i);

                //loop through
                foreach (DataRow dr in dsFlights.Tables[0].Rows)
                {
                    //create new flight
                    clsFlight clsMyFlight = new clsFlight();
                    //fill it up
                    clsMyFlight.sFlightID = int.Parse(dr["Flight_ID"].ToString());
                    clsMyFlight.FlightNumber = int.Parse(dr["Flight_Number"].ToString());
                    clsMyFlight.AircraftType = (string)dr["Aircraft_Type"];
                    //add list of flights
                    flights.Add(clsMyFlight);
                }
                //return
                return flights;

            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }




        /// <summary>
        /// Handle the error.
        /// </summary>
        /// <param name="sClass">The class in which the error occurred in.</param>
        /// <param name="sMethod">The method in which the error occurred in.</param>
        private void HandleError(string sClass, string sMethod, string sMessage)
        {
            try
            {
                //Would write to a file or database here.
                MessageBox.Show(sClass + "." + sMethod + " -> " + sMessage);
            }
            catch (Exception ex)
            {
                System.IO.File.AppendAllText("C:\\Error.txt", Environment.NewLine +
                                             "HandleError Exception: " + ex.Message);
            }
        }
    }
}
