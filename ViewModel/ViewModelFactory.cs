using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkyStopwatch.ViewModel
{
    public class ViewModelFactory
    {
        public static ViewModelFactory Instance { get; } = new ViewModelFactory();

        private ViewModelFactory()
        {
            
        }


        //leotodo, put theme here for now, not a good practice
        private OCRGameTime GameTime { get; } = new OCRGameTime();

        public OCRGameTime GetGameTime()
        {
            return GameTime;
        }

        public OCRBossCounting BossCounting { get; } = new OCRBossCounting();

        public OCRPrice Price { get; } = new OCRPrice();



    }
}
