using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SkyStopwatch
{
  

    /*
     
    ## notes
        - 2 kinds of samples to OCR image
            - from disk, manually triggered by button clcik, will save a pic into tmp folder, slow
            - from memory, auto trigger, fast, does not save file




    ## todo

    [done] ### new game start - no timestamp

    [done-half] ### auto run ocr - so no need to manual click button every time into a new game
        - can not go full auto, to complicated - done now, directly read from memory, not disk, so it's worth to always run on background

    [done-most]### configurable fields
        - ocr block args (x,y, width, height)
            - set default value
            - user can update them
        - save pin/unpin status - thus no need to click it every time program restart - low prority
        - time adjusted - low prority

    [done] ### read img(sreen shot) from memory instead of disk

    ### remember last close location
     
    ### support multi screens ?

    ### button ocr not working when top most(pin) is off

    
    
    ## fixed issues

    ### tesseract not working
            - reference failed -> update .net framework to 4.7 (was 4.0), then reinstall tesseract, tesseract-OCR from nuget
            - tessact init error -> init args error, correct them, point to the right tessdata
     
     
     
     
     
     
     
     */
}
