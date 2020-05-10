using Business.Abstract;
using Core.Constants;
using DataAccess.Abstract;
using Entities.Concrete;
using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class AdvertisementService : IAdvertisementService
    {
        private IAdvertisementDal _advertisementDal;

        public AdvertisementService(IAdvertisementDal advertisementDal)
        {
            _advertisementDal = advertisementDal;
        }

        private Advertisement GetAdvertisementByInnerText(string innerText)
        {
            try
            {
                var splitted = innerText.Split('\n');
                var trimmedSplitted = string.Empty;

                for (int i = 0; i < splitted.Length; i++)
                {
                    if (string.IsNullOrWhiteSpace(splitted[i]))
                        continue;

                    trimmedSplitted += splitted[i].Trim() + Environment.NewLine;
                }

                var adsSplitted = trimmedSplitted.Split('\n');

                if (adsSplitted.Length < 5)
                    return null;

                return new Advertisement()
                {
                    Title = adsSplitted[0],
                    City = adsSplitted[1],
                    Company = adsSplitted[2],
                    Sector = adsSplitted[3],
                    Date = DateTime.Parse(adsSplitted[4]),
                    Age = "",
                    Gender = "",
                    District = "",
                    Fax = "",
                    Phone = "",
                    AdsDetail = "",
                    RelatedPerson = "",
                    WorkingTime = ""
                };
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task GetData()
        {
            var httpClient = new HttpClient();
            var ads = new List<Advertisement>();

            for (int i = 1; i < 57; i++)
            {
                var url = string.Format(Values.URL, i);
                var html = await httpClient.GetStringAsync(url);
                var htmlDocument = new HtmlDocument();
                htmlDocument.LoadHtml(html);
                var adsNodes = htmlDocument.DocumentNode.SelectSingleNode(Values.XPATH_FOR_URL).ChildNodes[1].ChildNodes;

                for (int k = 0; k < adsNodes.Count; k++)
                {
                    if (k % 2 == 0 || k == 1)
                        continue;

                    var mainNode = adsNodes[k];
                    var adsBasicModel = GetAdvertisementByInnerText(mainNode.InnerText);

                    if (adsBasicModel != null)
                        ads.Add(adsBasicModel);
                }
            }

            foreach (var adsModel in ads)
            {
                _advertisementDal.Add(adsModel);
            }
        }
    }
}