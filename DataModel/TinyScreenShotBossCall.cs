using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkyStopwatch.DataModel
{
    public class TinyScreenShotBossCall: TinyScreenShot
    {
        public byte[] AUXData { get; set; }

        public DateTime ConsumeAt { get; set; }
        public TinyScreenShotBossCall(byte[] data, byte[] auxData)
        {
            this.Id = GlobalData.Default.ScreenShotSeedBossCall++;
            this.CreateAt = DateTime.UtcNow; //it's said utcnow is more accrute than datetime.now
            this.Data = data;
            this.AUXData = auxData;
        }
    }
}
