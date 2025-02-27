using Fastenshtein;
using Newtonsoft.Json;
using System.Linq;




namespace PizzaDB.utils
{
    public static class LevenshteinTest
    {
        //public static Dictionary<string, int> testc(AddressDTO value1, AddressResDTO[] Addresses)
        //{
        //    Fastenshtein.Levenshtein levA = new Fastenshtein.Levenshtein(value1.Address);
        //    Fastenshtein.Levenshtein levC = new Fastenshtein.Levenshtein(value1.City);

        //    var res = Addresses.ToDictionary(x => JsonConvert.SerializeObject(x), x => levA.DistanceFrom(x.Address) + (x.City == value1.City ? 0 : levC.DistanceFrom(x.City)));
        //    //ordina per i piu simili dello stesso paese poi i piu simili in generale
        //    return res.OrderBy(x => JsonConvert.DeserializeObject<AddressResDTO>(x.Key).City == value1.City ? 0 : 1)
        //              .ThenBy(x => x.Value)
        //              .ToDictionary(x => x.Key, x => x.Value);

        //}
        public static List<AddressResDTO> testc(AddressDTO value1, AddressResDTO[] Addresses)
        {
            var addressWords = value1.Address;

            int CustomLevenshtein(string word1, string word2)
            {
                word1 = word1.Replace(" ", "");
                word2 = word2.Replace(" ", "");
                int minLength = Math.Min(word1.Length, word2.Length);
                word1 = word1.Substring(0, minLength);
                word2 = word2.Substring(0, minLength);

                return Levenshtein.Distance(word1, word2);
            }



            var res = Addresses.ToDictionary(x => x, x =>
            {
                var addressArray = x.Address;

                return CustomLevenshtein(addressWords, addressArray);
            });

            var orderedRes = res.OrderBy(x =>
            {
                //int cityMatch = x.Key.City.Equals(value1.City, StringComparison.OrdinalIgnoreCase) ? 0 : 1;

                int similarity = x.Value;

                return (similarity);
            }).ToList();

            var orderedAddresses = orderedRes.Select(x => x.Key).ToList();

            return orderedAddresses;

        }
    }
}
