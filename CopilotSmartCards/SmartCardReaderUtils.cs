using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Windows.Devices.Enumeration;
using Windows.Devices.SmartCards;

public class SmartCardReaderUtils
{
    /// <summary>
    /// Checks whether the device supports smart card reader mode
    /// </summary>
    /// <returns>None</returns>
    public static async Task<DeviceInformation> GetFirstSmartCardReaderInfo(SmartCardReaderKind readerKind = SmartCardReaderKind.Any)
    {
        // Check if the SmartCardConnection API exists on this currently running SKU of Windows
        if (!Windows.Foundation.Metadata.ApiInformation.IsTypePresent("Windows.Devices.SmartCards.SmartCardConnection"))
        {
            // This SKU of Windows does not support NFC card reading, only desktop and phones/mobile devices support NFC card reading
            return null;
        }

        // Device selector string is from SmartCardReader.GetDeviceSelector(SmartCardReaderKind.Nfc) except that we remove the conditional
        // about the interface being enabled, since we want to get the device object regardless of whether the user turned it off in the CPL
        string query = "System.Devices.InterfaceClassGuid:=\"{DEEBE6AD-9E01-47E2-A3B2-A66AA2C036C9}\"";
        if (readerKind != SmartCardReaderKind.Any)
        {
            query += " AND System.Devices.SmartCards.ReaderKind:=" + (int)readerKind;
        }

        DeviceInformationCollection devices = await DeviceInformation.FindAllAsync(query);

        // There is a bug on some devices that were updated to WP8.1 where an NFC SmartCardReader is
        // enumerated despite that the device does not support it. As a workaround, we can do an additonal check
        // to ensure the device truly does support it.
        var workaroundDetect = await DeviceInformation.FindAllAsync("System.Devices.InterfaceClassGuid:=\"{50DD5230-BA8A-11D1-BF5D-0000F805F530}\"");

        if (workaroundDetect.Count == 0 || devices.Count == 0)
        {
            // Not supported
            return null;
        }

        // Smart card reader supported, but may be disabled
        return (from d in devices
                where d.IsEnabled
                orderby d.IsDefault descending
                select d).FirstOrDefault();
    }

    public static async Task<DeviceInformationCollection> GetAllSmartCardReaderInfos(SmartCardReaderKind readerKind = SmartCardReaderKind.Any)
    {
        // Check if the SmartCardConnection API exists on this currently running SKU of Windows
        if (!Windows.Foundation.Metadata.ApiInformation.IsTypePresent("Windows.Devices.SmartCards.SmartCardConnection"))
        {
            // This SKU of Windows does not support NFC card reading, only desktop and phones/mobile devices support NFC card reading
            return null;
        }

        // Device selector string is from SmartCardReader.GetDeviceSelector(SmartCardReaderKind.Nfc) except that we remove the conditional
        // about the interface being enabled, since we want to get the device object regardless of whether the user turned it off in the CPL
        string query = "System.Devices.InterfaceClassGuid:=\"{DEEBE6AD-9E01-47E2-A3B2-A66AA2C036C9}\"";
        if (readerKind != SmartCardReaderKind.Any)
        {
            query += " AND System.Devices.SmartCards.ReaderKind:=" + (int)readerKind;
        }

        DeviceInformationCollection devices = await DeviceInformation.FindAllAsync(query);

        // There is a bug on some devices that were updated to WP8.1 where an NFC SmartCardReader is
        // enumerated despite that the device does not support it. As a workaround, we can do an additonal check
        // to ensure the device truly does support it.
        var workaroundDetect = await DeviceInformation.FindAllAsync("System.Devices.InterfaceClassGuid:=\"{50DD5230-BA8A-11D1-BF5D-0000F805F530}\"");

        if (workaroundDetect.Count == 0 || devices.Count == 0)
        {
            // Not supported
            return null;
        }

        return devices;
    }
}