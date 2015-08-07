using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using EdmundsApi.Responses;
using EdmundsApi.Models;
using System.Collections.Generic;
using EdmundsApi.Requests;
using Flurl.Http.Testing;
using EdmundsApi;

namespace EdmundsApi.Test
{
    [TestClass]
    public class EdmundsTests
    {
        private static string _apiKey = "noKey";
        private Edmunds _api;

        // Set _mockHttp to false to actually call the service
        private static bool _mockHttp = false;
        private HttpTest _httpTest;

        [ClassInitialize]
        public static void ClassInit(TestContext testContext)
        {
            if (!_mockHttp)
                _apiKey = System.IO.File.ReadAllText("EdmundsApiKey.txt").Trim();
        }

        [TestInitialize]
        public void Init()
        {
            _api = new Edmunds(_apiKey);

            if (_mockHttp)
            {
                //Flurl is in test mode
                _httpTest = new HttpTest();
            }
        }

        [TestCleanup]
        public void Cleanup()
        {
            if (_mockHttp)
            {
                _httpTest.Dispose();
            }
        }

        [TestMethod]
        public void AssumeStyleIsNotNull()
        {
            var vId = 200487199;
            SetHttpResponse(styleDefault);
            var style = _api.GetStyleInfo(vId).Result;

            Assert.IsNotNull(style);
            Assert.AreEqual("EX-L V6 w/Navigation 4dr Sedan (3.5L 6cyl 6A)", style.Name);
        }

        [TestMethod]
        public void MakeCall_GetAllMakes()
        {
            SetHttpResponse(listOfMakes);
            var makeList = _api.Call<MakeList>("/api/vehicle/v2/makes?state=new").Result;

            Assert.IsNotNull(makeList);
            Assert.AreEqual("Acura", makeList.makes[0].Name);
        }

        [TestMethod]
        public void ShouldGetAccordData()
        {
            SetHttpResponse(modelIsAccord);
            var accordData = _api.Call<Model>("/api/vehicle/v2/{niceName}/{model}", new {niceName = "honda", model = "accord"}).Result;

            Assert.IsNotNull(accordData);
            Assert.AreEqual("Accord", accordData.Name);
        }

        [TestMethod]
        public void TestGetModelMethod()
        {
            SetHttpResponse(accord2015TestData);
            Edmunds ed = new Edmunds(_apiKey);
            var returnValue = ed.GetModelInfo("honda", "accord", "", 2015, Category.Sedan, State.New, View.Full);

            Assert.IsNotNull(returnValue);
            Assert.AreEqual("Accord", returnValue.Result.Name);
        }

        [TestMethod]
        public void MakeCall_GetMakeByName()
        {
            SetHttpResponse(makeIsToyota);
            var make = _api.Call<Make>("/api/vehicle/v2/{niceName}", new {niceName = "toyota"}).Result;

            Assert.IsNotNull(make);
            Assert.AreEqual("Toyota", make.Name);
        }

        [TestMethod]
        public void GetAllMakes_GetAllNew()
        {
            SetHttpResponse(listOfMakes);
            var makeList = _api.GetAllMakes(state: Requests.State.New).Result;

            Assert.IsNotNull(makeList);
        }

        //Helpers
        private void SetHttpResponse(string body)
        {
            if (_mockHttp)
            {
                _httpTest.RespondWith(body);
            }
        }

        private void SetHttpResponse(int status, string body)
        {
            if (_mockHttp)
            {
                _httpTest.RespondWith(status, body);
            }
        }

        #region ResponseJson
        string listOfMakes = @"{'makes':[{'id':200002038,'name':'Acura','niceName':'acura','models':[{'id':'Acura_ILX','name':'ILX','niceName':'ilx','years':[{'id':200471908,'year':2014},{'id':200701415,'year':2015},{'id':200713715,'year':2016}]},{'id':'Acura_ILX_Hybrid','name':'ILX Hybrid','niceName':'ilx-hybrid','years':[{'id':200493809,'year':2014}]},{'id':'Acura_MDX','name':'MDX','niceName':'mdx','years':[{'id':200465929,'year':2014},{'id':200698434,'year':2015},{'id':200726800,'year':2016}]},{'id':'Acura_RDX','name':'RDX','niceName':'rdx','years':[{'id':200467168,'year':2014},{'id':200693511,'year':2015},{'id':200727186,'year':2016}]},{'id':'Acura_RLX','name':'RLX','niceName':'rlx','years':[{'id':100539511,'year':2014},{'id':200706522,'year':2015},{'id':200729233,'year':2016}]},{'id':'Acura_TL','name':'TL','niceName':'tl','years':[{'id':200488448,'year':2014}]},{'id':'Acura_TLX','name':'TLX','niceName':'tlx','years':[{'id':200673634,'year':2015}]},{'id':'Acura_TSX','name':'TSX','niceName':'tsx','years':[{'id':200490517,'year':2014}]},{'id':'Acura_TSX_Sport_Wagon','name':'TSX Sport Wagon','niceName':'tsx-sport-wagon','years':[{'id':200673755,'year':2014}]}]},{'id':200464140,'name':'Alfa Romeo','niceName':'alfa-romeo','models':[{'id':'Alfa_Romeo_4C','name':'4C','niceName':'4c','years':[{'id':200700684,'year':2015}]}]},{'id':200001769,'name':'Aston Martin','niceName':'aston-martin','models':[{'id':'Aston_Martin_DB9','name':'DB9','niceName':'db9','years':[{'id':200473436,'year':2014}]},{'id':'Aston_Martin_Rapide_S','name':'Rapide S','niceName':'rapide-s','years':[{'id':200460643,'year':2014}]},{'id':'Aston_Martin_V12_Vantage_S','name':'V12 Vantage S','niceName':'v12-vantage-s','years':[{'id':200693539,'year':2015}]},{'id':'Aston_Martin_V8_Vantage','name':'V8 Vantage','niceName':'v8-vantage','years':[{'id':200472947,'year':2014}]},{'id':'Aston_Martin_Vanquish','name':'Vanquish','niceName':'vanquish','years':[{'id':200431313,'year':2014}]}]}],'makesCount':3}";

        string makeIsToyota = @"{'id':200003381,'name':'Toyota','niceName':'toyota','models':[{'id':'Toyota_4Runner','name':'4Runner','niceName':'4runner','years':[{'id':200470521,'year':2014},{'id':200710304,'year':2015}]},{'id':'Toyota_Avalon','name':'Avalon','niceName':'avalon','years':[{'id':200493856,'year':2014},{'id':200718729,'year':2015}]},{'id':'Toyota_Avalon_Hybrid','name':'Avalon Hybrid','niceName':'avalon-hybrid','years':[{'id':200493834,'year':2014},{'id':200718730,'year':2015}]},{'id':'Toyota_Camry','name':'Camry','niceName':'camry','years':[{'id':200485954,'year':2014},{'id':200693837,'year':2015}]},{'id':'Toyota_Camry_Hybrid','name':'Camry Hybrid','niceName':'camry-hybrid','years':[{'id':200485955,'year':2014},{'id':200711464,'year':2015}]},{'id':'Toyota_Corolla','name':'Corolla','niceName':'corolla','years':[{'id':200465937,'year':2014},{'id':200707275,'year':2015}]},{'id':'Toyota_FJ_Cruiser','name':'FJ Cruiser','niceName':'fj-cruiser','years':[{'id':200490776,'year':2014}]},{'id':'Toyota_Highlander','name':'Highlander','niceName':'highlander','years':[{'id':200465938,'year':2014},{'id':200706712,'year':2015}]},{'id':'Toyota_Highlander_Hybrid','name':'Highlander Hybrid','niceName':'highlander-hybrid','years':[{'id':200498461,'year':2014},{'id':200708166,'year':2015}]},{'id':'Toyota_Land_Cruiser','name':'Land Cruiser','niceName':'land-cruiser','years':[{'id':200494245,'year':2014},{'id':200710728,'year':2015}]},{'id':'Toyota_Mirai','name':'Mirai','niceName':'mirai','years':[{'id':200711329,'year':2016}]},{'id':'Toyota_Prius','name':'Prius','niceName':'prius','years':[{'id':200492958,'year':2014},{'id':200474302,'year':2015}]},{'id':'Toyota_Prius_Plug_in','name':'Prius Plug-in','niceName':'prius-plug-in','years':[{'id':200495857,'year':2014},{'id':200710957,'year':2015}]},{'id':'Toyota_Prius_c','name':'Prius c','niceName':'prius-c','years':[{'id':200496060,'year':2014},{'id':200722835,'year':2015}]},{'id':'Toyota_Prius_v','name':'Prius v','niceName':'prius-v','years':[{'id':200490605,'year':2014},{'id':200722020,'year':2015}]},{'id':'Toyota_RAV4','name':'RAV4','niceName':'rav4','years':[{'id':200433886,'year':2014},{'id':200710542,'year':2015}]},{'id':'Toyota_RAV4_EV','name':'RAV4 EV','niceName':'rav4-ev','years':[{'id':200495460,'year':2014}]},{'id':'Toyota_Sequoia','name':'Sequoia','niceName':'sequoia','years':[{'id':200486611,'year':2014},{'id':200710307,'year':2015}]},{'id':'Toyota_Sienna','name':'Sienna','niceName':'sienna','years':[{'id':200489889,'year':2014},{'id':200705264,'year':2015}]},{'id':'Toyota_Tacoma','name':'Tacoma','niceName':'tacoma','years':[{'id':200491998,'year':2014},{'id':200708472,'year':2015}]},{'id':'Toyota_Tundra','name':'Tundra','niceName':'tundra','years':[{'id':200466835,'year':2014},{'id':200711788,'year':2015}]},{'id':'Toyota_Venza','name':'Venza','niceName':'venza','years':[{'id':200488728,'year':2014},{'id':200708942,'year':2015}]},{'id':'Toyota_Yaris','name':'Yaris','niceName':'yaris','years':[{'id':200482940,'year':2014},{'id':200705964,'year':2015}]}]}";

        string modelIsAccord = @"{'id':'Honda_Accord','name':'Accord','niceName':'accord','years':[{'id':200487197,'year':2014,'styles':[{'id':200487198,'name':'EX-L w/Navigation 2dr Coupe (2.4L 4cyl CVT)','submodel':{'body':'Coupe','modelName':'Accord Coupe','niceName':'coupe'},'trim':'EX-L w/Navigation'},{'id':200487199,'name':'EX-L V6 w/Navigation 4dr Sedan (3.5L 6cyl 6A)','submodel':{'body':'Sedan','modelName':'Accord Sedan','niceName':'sedan'},'trim':'EX-L V-6 w/Navigation'},{'id':200487218,'name':'LX-S 2dr Coupe (2.4L 4cyl 6M)','submodel':{'body':'Coupe','modelName':'Accord Coupe','niceName':'coupe'},'trim':'LX-S'},{'id':200487216,'name':'EX 2dr Coupe (2.4L 4cyl 6M)','submodel':{'body':'Coupe','modelName':'Accord Coupe','niceName':'coupe'},'trim':'EX'},{'id':200487217,'name':'LX-S 2dr Coupe (2.4L 4cyl CVT)','submodel':{'body':'Coupe','modelName':'Accord Coupe','niceName':'coupe'},'trim':'LX-S'},{'id':200487211,'name':'EX-L V-6 4dr Sedan (3.5L 6cyl 6A)','submodel':{'body':'Sedan','modelName':'Accord Sedan','niceName':'sedan'},'trim':'EX-L V-6'},{'id':200487210,'name':'EX-L V-6 2dr Coupe (3.5L 6cyl 6A)','submodel':{'body':'Coupe','modelName':'Accord Coupe','niceName':'coupe'},'trim':'EX-L V-6'},{'id':200487209,'name':'EX-L w/Navigation 4dr Sedan (2.4L 4cyl CVT)','submodel':{'body':'Sedan','modelName':'Accord Sedan','niceName':'sedan'},'trim':'EX-L w/Navigation'},{'id':200487208,'name':'EX-L V6 w/Navigation 2dr Coupe (3.5L 6cyl 6M)','submodel':{'body':'Coupe','modelName':'Accord Coupe','niceName':'coupe'},'trim':'EX-L V-6 w/Navigation'},{'id':200487215,'name':'EX 4dr Sedan (2.4L 4cyl 6M)','submodel':{'body':'Sedan','modelName':'Accord Sedan','niceName':'sedan'},'trim':'EX'},{'id':200487214,'name':'LX 4dr Sedan (2.4L 4cyl 6M)','submodel':{'body':'Sedan','modelName':'Accord Sedan','niceName':'sedan'},'trim':'LX'},{'id':200487213,'name':'Sport 4dr Sedan (2.4L 4cyl CVT)','submodel':{'body':'Sedan','modelName':'Accord Sedan','niceName':'sedan'},'trim':'Sport'},{'id':200487212,'name':'Sport 4dr Sedan (2.4L 4cyl 6M)','submodel':{'body':'Sedan','modelName':'Accord Sedan','niceName':'sedan'},'trim':'Sport'},{'id':200487203,'name':'EX-L 2dr Coupe (2.4L 4cyl CVT)','submodel':{'body':'Coupe','modelName':'Accord Coupe','niceName':'coupe'},'trim':'EX-L'},{'id':200487202,'name':'EX 2dr Coupe (2.4L 4cyl CVT)','submodel':{'body':'Coupe','modelName':'Accord Coupe','niceName':'coupe'},'trim':'EX'},{'id':200487201,'name':'Touring V-6 4dr Sedan (3.5L 6cyl 6A)','submodel':{'body':'Sedan','modelName':'Accord Sedan','niceName':'sedan'},'trim':'Touring V-6'},{'id':200487200,'name':'EX-L V-6 2dr Coupe (3.5L 6cyl 6M)','submodel':{'body':'Coupe','modelName':'Accord Coupe','niceName':'coupe'},'trim':'EX-L V-6'},{'id':200487207,'name':'EX-L 4dr Sedan (2.4L 4cyl CVT)','submodel':{'body':'Sedan','modelName':'Accord Sedan','niceName':'sedan'},'trim':'EX-L'},{'id':200487206,'name':'LX 4dr Sedan (2.4L 4cyl CVT)','submodel':{'body':'Sedan','modelName':'Accord Sedan','niceName':'sedan'},'trim':'LX'},{'id':200487205,'name':'EX 4dr Sedan (2.4L 4cyl CVT)','submodel':{'body':'Sedan','modelName':'Accord Sedan','niceName':'sedan'},'trim':'EX'},{'id':200487204,'name':'EX-L V6 w/Navigation 2dr Coupe (3.5L 6cyl 6A)','submodel':{'body':'Coupe','modelName':'Accord Coupe','niceName':'coupe'},'trim':'EX-L V-6 w/Navigation'}]}]}";

        string accord2015TestData = @"{'id':'Honda_Accord','name':'Accord','niceName':'accord','years':[{'id':200709376,'year':2015,'styles':[{'id':200709386,'name':'LX 4dr Sedan (2.4L 4cyl CVT)','submodel':{'body':'Sedan','modelName':'Accord Sedan','niceName':'sedan'},'trim':'LX'},{'id':200709387,'name':'EX 4dr Sedan (2.4L 4cyl CVT)','submodel':{'body':'Sedan','modelName':'Accord Sedan','niceName':'sedan'},'trim':'EX'},{'id':200709384,'name':'Touring V-6 4dr Sedan (3.5L 6cyl 6A)','submodel':{'body':'Sedan','modelName':'Accord Sedan','niceName':'sedan'},'trim':'Touring V-6'},{'id':200709385,'name':'EX-L 4dr Sedan (2.4L 4cyl CVT)','submodel':{'body':'Sedan','modelName':'Accord Sedan','niceName':'sedan'},'trim':'EX-L'},{'id':200709382,'name':'Sport 4dr Sedan (2.4L 4cyl 6M)','submodel':{'body':'Sedan','modelName':'Accord Sedan','niceName':'sedan'},'trim':'Sport'},{'id':200709383,'name':'EX-L V6 w/Navigation 4dr Sedan (3.5L 6cyl 6A)','submodel':{'body':'Sedan','modelName':'Accord Sedan','niceName':'sedan'},'trim':'EX-L V-6 w/Navigation'},{'id':200709380,'name':'LX 4dr Sedan (2.4L 4cyl 6M)','submodel':{'body':'Sedan','modelName':'Accord Sedan','niceName':'sedan'},'trim':'LX'},{'id':200709381,'name':'Sport 4dr Sedan (2.4L 4cyl CVT)','submodel':{'body':'Sedan','modelName':'Accord Sedan','niceName':'sedan'},'trim':'Sport'},{'id':200709378,'name':'EX-L w/Navigation 4dr Sedan (2.4L 4cyl CVT)','submodel':{'body':'Sedan','modelName':'Accord Sedan','niceName':'sedan'},'trim':'EX-L w/Navigation'},{'id':200709379,'name':'EX 4dr Sedan (2.4L 4cyl 6M)','submodel':{'body':'Sedan','modelName':'Accord Sedan','niceName':'sedan'},'trim':'EX'},{'id':200709377,'name':'EX-L V-6 4dr Sedan (3.5L 6cyl 6A)','submodel':{'body':'Sedan','modelName':'Accord Sedan','niceName':'sedan'},'trim':'EX-L V-6'}]}]}";

        string styleDefault = @"{'make':{'id':200001444,'name':'Honda','niceName':'honda'},'model':{'id':'Honda_Accord','name':'Accord','niceName':'accord'},'engine':{'id':'200487224','name':'V6','equipmentType':'ENGINE','compressionRatio':10.5,'cylinder':6,'size':3.5,'displacement':3471.0,'configuration':'V','fuelType':'regular unleaded','horsepower':278,'torque':252,'totalValves':24,'type':'gas','code':'6VNAG3.5','compressorType':'NA','rpm':{'horsepower':6200,'torque':4900},'valve':{'timing':'variable valve timing','gear':'single overhead camshaft'}},'transmission':{'id':'200487229','name':'6A','equipmentType':'TRANSMISSION','transmissionType':'AUTOMATIC','numberOfSpeeds':'6'},'drivenWheels':'front wheel drive','numOfDoors':'4','options':[],'colors':[{'category':'Interior','options':[{'id':'200487235','name':'Black Leather','equipmentType':'COLOR','manufactureOptionName':'Black','manufactureOptionCode':'BK','colorChips':{'primary':{'r':25,'g':25,'b':25,'hex':'191919'}},'fabricTypes':[{'name':'FABRIC_TYPE_1','value':'leather'}]},{'id':'200487248','name':'Ivory Leather','equipmentType':'COLOR','manufactureOptionName':'Ivory','manufactureOptionCode':'IV/BI','colorChips':{'primary':{'r':156,'g':141,'b':126,'hex':'9C8D7E'}},'fabricTypes':[{'name':'FABRIC_TYPE_1','value':'leather'}]},{'id':'200487247','name':'Gray Leather','equipmentType':'COLOR','manufactureOptionName':'Gray','manufactureOptionCode':'GR','colorChips':{'primary':{'r':121,'g':121,'b':121,'hex':'797979'}},'fabricTypes':[{'name':'FABRIC_TYPE_1','value':'leather'}]}]},{'category':'Exterior','options':[{'id':'200487243','name':'Obsidian Blue Pearl','equipmentType':'COLOR','manufactureOptionName':'Obsidian Blue Pearl','manufactureOptionCode':'BS/BU','colorChips':{'primary':{'r':12,'g':34,'b':92,'hex':'0C225C'}}},{'id':'200487244','name':'Champagne Frost Pearl','equipmentType':'COLOR','manufactureOptionName':'Champagne Frost Pearl','manufactureOptionCode':'BE/BG','colorChips':{'primary':{'r':200,'g':167,'b':124,'hex':'C8A77C'}}},{'id':'200487242','name':'Alabaster Silver Metallic','equipmentType':'COLOR','manufactureOptionName':'Alabaster Silver Metallic','manufactureOptionCode':'SI/SX','colorChips':{'primary':{'r':203,'g':205,'b':204,'hex':'CBCDCC'}}},{'id':'200487240','name':'Hematite Metallic','equipmentType':'COLOR','manufactureOptionName':'Hematite Metallic','manufactureOptionCode':'GN','colorChips':{'primary':{'r':74,'g':73,'b':69,'hex':'4A4945'}}},{'id':'200487233','name':'Crystal Black Pearl','equipmentType':'COLOR','manufactureOptionName':'Crystal Black Pearl','manufactureOptionCode':'BK/BX','colorChips':{'primary':{'r':29,'g':29,'b':29,'hex':'1D1D1D'}}},{'id':'200487236','name':'White Orchid Pearl','equipmentType':'COLOR','manufactureOptionName':'White Orchid Pearl','manufactureOptionCode':'WA/WB','colorChips':{'primary':{'r':251,'g':251,'b':240,'hex':'FBFBF0'}}},{'id':'200487238','name':'Modern Steel Metallic','equipmentType':'COLOR','manufactureOptionName':'Modern Steel Metallic','manufactureOptionCode':'GX/GY','colorChips':{'primary':{'r':51,'g':59,'b':72,'hex':'333B48'}}},{'id':'200487234','name':'Basque Red Pearl II','equipmentType':'COLOR','manufactureOptionName':'Basque Red Pearl II','manufactureOptionCode':'RA/RB','colorChips':{'primary':{'r':109,'g':42,'b':49,'hex':'6D2A31'}}}]}],'manufacturerCode':'CR3F8EKNW','price':{'baseMSRP':32120.0,'baseInvoice':29372.0,'deliveryCharges':820.0,'usedTmvRetail':25357.0,'usedPrivateParty':24086.0,'usedTradeIn':22400.0,'estimateTmv':false},'categories':{'market':'N/A','EPAClass':'Midsize Cars','vehicleSize':'Midsize','primaryBodyType':'Car','vehicleStyle':'Sedan','vehicleType':'Car'},'id':200487199,'name':'EX-L V6 w/Navigation 4dr Sedan (3.5L 6cyl 6A)','year':{'id':200487197,'year':2014},'submodel':{'body':'Sedan','modelName':'Accord Sedan','niceName':'sedan'},'trim':'EX-L V-6 w/Navigation','states':['USED','NEW'],'squishVins':['1HGCR3F8EA'],'MPG':{'highway':'34','city':'21'}}";

        #endregion
    }
}
