using System;

namespace BtsMsiLib.Model
{
    public class BtsApplicationValidator
    {
        public static void Validate(BtsApplication btsApplication)
        {
            if (btsApplication == null)
                throw new ArgumentNullException("btsApplication", "BtsApplication cannot be null");

            if (string.IsNullOrEmpty(btsApplication.Name))
                throw new ArgumentException("BizTalk Application Name cannot be null or empty");
        }
    }
}
