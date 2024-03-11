using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Assignment6Flight
{
    internal class clsPassenger
    {
        //PassengerID, FirstName, LastName
        /// <summary>
        /// variable for the passengerID
        /// </summary>
        public int PassengerID { get; set; }

        /// <summary>
        /// variable for the FirstName of the passenger
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// variable for the Lastname of the passenger
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// variable for the seat number of a passenger
        /// </summary>
        public string SeatNum { get; set; }

        /// <summary>
        /// variable for the flight number associated with a passenger
        /// </summary>
        public string sFlight { get; set; }

        /// <summary>
        /// overrides the ToString() method for the first and last name
        /// </summary>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public override string ToString()
        {
            try
            {
                return FirstName + " " + LastName;
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
