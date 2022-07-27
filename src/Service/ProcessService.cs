using System;
using LINQSamples.src.Accounting;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace LINQSamples.service
{


    public class ProcessService 
    {
        IAccountant _accountant;
        public ProcessService()
        {
            
        }

        public void Run(IAccountant accountant)
        {
            try
            {


            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}