using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using MSI.Afterburner;
using MSI.Afterburner.Exceptions;


namespace afterburner 
{
	public class LCDSmartie 
    {
        public string function1(string param1, string param2) 
        {
            try
            {
                HardwareMonitor mahm = new HardwareMonitor();
                if (param1 == "Framerate")
                {
                    HardwareMonitorEntry framerate = mahm.GetEntry(HardwareMonitor.GPU_GLOBAL_INDEX, MONITORING_SOURCE_ID.FRAMERATE);
                    if (framerate == null)  return "---";
                    return framerate.Data.ToString(param2);
                }
                HardwareMonitorEntry value = mahm.GetEntry(0, param1);
                mahm.ReloadEntry(value);
                return value.Data.ToString(param2);
            }

            catch (MSI.Afterburner.Exceptions.SharedMemoryDead) { return "[Exception: Shared memory dead because Afterburner was closed. You have to restart LCD Smartie."; } //TODO: srsly
            catch (MSI.Afterburner.Exceptions.SharedMemoryNotFound) { return "[Exception: Couldn't find shared memory. Is Afterburner running?]"; }
            catch (Exception e) { return e.ToString(); }

        }
        public int GetMinRefreshInterval() { return 300; }
	}
}
