using System.Text.RegularExpressions;

namespace catalog.infra.RabbitMQ
{
    public static class TratarMenssagem
    {
        public static string RecuperarValor(string msg, string tagDesejada)
        {
            var match = Regex.Match(msg, $"<{tagDesejada}>(.*?)</{tagDesejada}>");
            if (match.Success)
                return match.Groups[1].Value;
            return null;
        }
    }
}
