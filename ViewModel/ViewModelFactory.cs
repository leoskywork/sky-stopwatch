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
        private MainOCRGameTime GameTime { get; } = new MainOCRGameTime();

        public MainOCRGameTime GetGameTime()
        {
            return GameTime;
        }

        public MainOCRBossCounting BossCounting { get; } = new MainOCRBossCounting();

        public MainOCRPrice Price { get; } = new MainOCRPrice();



    }
}
