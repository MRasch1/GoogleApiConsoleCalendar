using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Google.Apis.Calendar.v3;
using Google.Apis.Services;

namespace test002
{
    internal class Program
    {
        const string ApiKey = "AIzaSyAfRLnIqo-P40tgSPqfGdzUwO1NQA-SAE4";
        const string CalendarId = "da.danish#holiday@group.v.calendar.google.com";
        static async Task Main(string[] args)
        {
            var prog = new Program();

            var service = new CalendarService(new BaseClientService.Initializer()
            {
                ApiKey = ApiKey,
                ApplicationName = "Api key example"
            });

            var request = service.Events.List(CalendarId);
            request.Fields = "items(summary,start,end)";
            var response = await request.ExecuteAsync();

            int targetYear = 2023;

            foreach (var item in response.Items)
            {
                string startDate = item.Start.Date;
                string endDate = item.End.Date;
                DateTime startYear = DateTime.Parse(item.Start.Date);
                DateTime endYear = DateTime.Parse(item.End.Date);


                if (startYear.Year == targetYear || endYear.Year == targetYear)
                {
                    Console.WriteLine($"Holiday: {item.Summary}. Start date: {prog.DateTimeParsing(startDate)}. End date: {prog.DateTimeParsing(endDate)}.\n");
                }

            }

            Console.ReadKey();
        }

        public string DateTimeParsing(string input)
        {
            var result = DateTime.Parse(input).ToString("dd-MM-yyyy");
            return result;
        }

    }
}
