using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExploreCalifornia.Models
{
    public class FormattingService
    {
        //vytvorenie metody na modifikovanie formatu datumu a jej vyuzivanie v ramci view umoznuje upravovat fotmat datumov v ramci celej appa
        public string AsReadableDate(DateTime date)
        {
            return date.ToString("d");
        }
    }
}
