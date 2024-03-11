using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Assignment6Flight
{
    /// <summary>
    /// purpose is to hold data
    /// </summary>
    internal class clsFlight
    {
        /// <summary>
        /// Variable for the flight ID
        /// </summary>
        public int sFlightID { get; set; }

        /// <summary>
        /// variable for the flight number
        /// </summary>
        public int FlightNumber { get; set; }

        /// <summary>
        /// variable for the aircraftType
        /// </summary>
        public string AircraftType { get; set; }


        /// <summary>
        /// overriding the ToString() method for the flight number and aircraft type
        /// </summary>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public override string ToString()
        {
            try
            {
                return FlightNumber + " - " + AircraftType;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                     MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
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
