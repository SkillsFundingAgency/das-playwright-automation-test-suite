namespace SFA.DAS.Framework.Helpers;

public class MailosaurDeviceHelper(string apiToken)
{
    public string GetCode(string deviceId)
    {
        var mailosaur = new MailosaurClient(apiToken);

        var result = mailosaur.Devices.Otp(deviceId);

        return result.Code;
    }
}
