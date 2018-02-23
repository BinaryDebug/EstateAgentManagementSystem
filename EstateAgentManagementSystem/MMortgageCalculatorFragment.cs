using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using System.Globalization;

namespace EstateAgentManagementSystem
{
    public class MMortgageCalculatorFragment : Fragment
    {
        EditText mortgageAmountEditText, interestRateEditText, mortgageTermEditText;
        TextView monthlyPaymentTextView;
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here

        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            SetHasOptionsMenu(true);

            // Use this to return your custom view for this Fragment
            // return inflater.Inflate(Resource.Layout.YourFragment, container, false);

            View view = LayoutInflater.From(Activity).Inflate(Resource.Layout.MMortgageCalculatorView, null);

            // Get our button from the layout resource,
            // and attach an event to it
            Button button = view.FindViewById<Button>(Resource.Id.calculateButton);
            mortgageAmountEditText = view.FindViewById<EditText>(Resource.Id.mortgageAmountEditText);
            interestRateEditText = view.FindViewById<EditText>(Resource.Id.interestRateEditText);
            mortgageTermEditText = view.FindViewById<EditText>(Resource.Id.mortgageTermEditText);
            monthlyPaymentTextView = view.FindViewById<TextView>(Resource.Id.monthlyPaymentTextView);

            button.Click += Button_Click;


            return view;
        }

        //On button clicked, do the calculation and display.
        private void Button_Click(object sender, EventArgs e)
        {
            CultureInfo cultureInfo = new CultureInfo("en-GB");
            try
            {
                double mortgageAmount = double.Parse(mortgageAmountEditText.Text);
                double interestRate = double.Parse(interestRateEditText.Text);
                int term = int.Parse(mortgageTermEditText.Text);

                monthlyPaymentTextView.Text = $"{String.Format(cultureInfo, "{0:C}", CalculateMonthlyPayment(mortgageAmount, interestRate, term))}";
            }
            catch (Exception)
            {
                //Who cares?
            }


        }

        //Returns the monthly mortgage payment depending on user's inputted values
        public static double CalculateMonthlyPayment(double poundsMortgageAmount, double percentageInterestRate, int yearsMortgageTerm)
        {
            double monthlyInterestRate = (percentageInterestRate / 100) / 12;
            double monthsMortgageTerm = yearsMortgageTerm * 12;

            double monthlyPayment = poundsMortgageAmount *
                                    (monthlyInterestRate * Math.Pow(1 + monthlyInterestRate, monthsMortgageTerm)) /
                                    (Math.Pow(1 + monthlyInterestRate, monthsMortgageTerm) - 1);

            return monthlyPayment;
        }


    }
}