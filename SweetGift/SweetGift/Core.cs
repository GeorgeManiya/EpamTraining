using System.IO;
using System.Xml.Linq;
using SweetGift.Interfaces;
using SweetGift.Data;
using System;

namespace SweetGift
{
    static class Core
    {
        public static Gift Gift { get; set; }

        private static XDocument _data;
        private static XElement _sweetGift;

        static Core()
        {
            if (!File.Exists("Data.xml"))
            {
                var _data = new XDocument();
                _sweetGift = new XElement("SweetGift");
                _data.Add(_sweetGift);
                _data.Save("Data.xml");
            }

            _data = XDocument.Load("Data.xml");
            if (_data.Element("SweetGift") == null)
            {
                _sweetGift = new XElement("SweetGift");
                _data.Add(_sweetGift);
            }
            else
            {
                _sweetGift = _data.Element("SweetGift");
            }

            Gift = new Gift();
            FillData();
        }

        private static void FillData()
        {
            if (!_sweetGift.HasElements) return;

            foreach(var element in _sweetGift.Elements())
            {
                switch(element.Name.LocalName)
                {
                    case "Biscuit":
                        {
                            var biscuit = new Biscuit();
                            GetGiftComponentPart(biscuit, element);
                            GetChocolatePart(biscuit, element);
                            Gift.Add(biscuit);
                        }
                        break;
                    case "Candy":
                        {
                            var candy = new Candy();
                            GetGiftComponentPart(candy, element);
                            GetChocolatePart(candy, element);
                            Gift.Add(candy);
                        }
                        break;
                    case "ChocolateEgg":
                        {
                            var chocolateEgg = new ChocolateEgg();
                            GetGiftComponentPart(chocolateEgg, element);
                            GetChocolatePart(chocolateEgg, element);
                            var toyElement = element.Element("Toy");
                            if(toyElement != null)
                            {
                                var toy = new InediblePart();
                                var madeOf = toyElement.Element("MadeOf");
                                if (madeOf != null)
                                {
                                    toy.MadeOf = (InediblePartType)Enum.Parse(typeof(InediblePartType), madeOf.Value);
                                }
                                var toyWeight = toyElement.Element("Weight");
                                if(toyWeight != null)
                                {
                                    toy.Weight = (int)toyWeight;
                                }
                                chocolateEgg.Toy = toy;
                            }
                            Gift.Add(chocolateEgg);
                        }
                        break;
                    case "Lollipop":
                        {
                            var lollipop = new Lollipop();
                            GetGiftComponentPart(lollipop, element);
                            var stickElement = element.Element("Stick");
                            if (stickElement != null)
                            {
                                var stick = new InediblePart();
                                var madeOf = stickElement.Element("MadeOf");
                                if (madeOf != null)
                                {
                                    stick.MadeOf = (InediblePartType)Enum.Parse(typeof(InediblePartType), madeOf.Value);
                                }
                                var stickWeight = stickElement.Element("Weight");
                                if (stickWeight != null)
                                {
                                    stick.Weight = (int)stickWeight;
                                }
                                lollipop.Stick = stick;
                            }
                            Gift.Add(lollipop);
                        }
                        break;
                }
            }
        }

        private static void GetGiftComponentPart(GiftComponent giftComponent, XElement element)
        {
            var weight = element.Element("Weight");
            if(weight != null)
            {
                giftComponent.Weight = (int)weight;
            }

            var sugar = element.Element("Sugar");
            if (sugar != null)
            {
                giftComponent.Sugar = (int)sugar;
            }

            var name = element.Element("Name");
            if (name != null)
            {
                giftComponent.Name = name.ToString();
            }

            var companyName = element.Element("CompanyName");
            if (companyName != null)
            {
                giftComponent.CompanyName = companyName.ToString();
            }

            var makingType = element.Attribute("MakingType");
            if(makingType != null)
            {
                giftComponent.MakingType = (GiftComponentMakingType)Enum.Parse(typeof(GiftComponentMakingType), makingType.Value);
            }

            var wrapper = element.Element("Wrapper");
            if (wrapper != null)
            {
                var wrap = new Wrapper();
                var wrapperType = wrapper.Attribute("WrapperType");
                if(wrapperType != null)
                {
                    wrap.WrapperType = (WrapperType)Enum.Parse(typeof(WrapperType), wrapperType.Value);
                }
                var wrapperWeight = wrapper.Element("Weight");
                if(wrapperWeight != null)
                {
                    wrap.Weight = (int)wrapperWeight;
                }

                giftComponent.Wrapper = wrap;
            }
        }

        private static void GetChocolatePart(IChocolate chocolateComponent, XElement element)
        {
            var chocolate = element.Element("Chocolate");
            if(chocolate != null)
            {
                chocolateComponent.Chocolate = (int)chocolate;
            }
        }




        public static void Save()
        {
            if (Gift == null) return;

            foreach(var giftComponent in Gift)
            {
                IGiftComponent component = giftComponent as Biscuit;
                if (component != null)
                {
                    var biscuit = new XElement("Biscuit");
                    AddGiftComponentPart(component, biscuit);
                    AddChocolatePart((IChocolate)component, biscuit);

                    _sweetGift.Add(biscuit);
                }

                component = giftComponent as Candy;
                if (component != null)
                {
                    var candy = new XElement("Candy");
                    AddGiftComponentPart(component, candy);
                    AddChocolatePart((IChocolate)component, candy);

                    _sweetGift.Add(candy);
                }

                component = giftComponent as ChocolateEgg;
                if (component != null)
                {
                    var chocolateEgg = new XElement("ChocolateEgg");
                    AddGiftComponentPart(component, chocolateEgg);
                    AddChocolatePart((IChocolate)component, chocolateEgg);

                    if (((ChocolateEgg)component).Toy != null)
                    {
                        chocolateEgg.Add(new XElement("Toy",
                                        new XAttribute("MadeOf", ((ChocolateEgg)component).Toy.MadeOf),
                                        new XElement("Weight", ((ChocolateEgg)component).Toy.Weight)));
                    }

                    _sweetGift.Add(chocolateEgg);
                }

                component = giftComponent as Lollipop;
                if (component != null)
                {
                    var lollipop = new XElement("Lollipop");
                    AddGiftComponentPart(component, lollipop);

                    if (((Lollipop)component).Stick != null)
                    {
                        lollipop.Add(new XElement("Stick",
                                        new XAttribute("MadeOf", ((Lollipop)component).Stick.MadeOf),
                                        new XElement("Weight", ((Lollipop)component).Stick.Weight)));
                    }

                    _sweetGift.Add(lollipop);
                }
            }

            _data.Save("Data.xml");
        }

        private static void AddGiftComponentPart(IGiftComponent giftComponent, XElement element)
        {
            if (giftComponent.Weight != 0)
            {
                element.Add(new XElement("Weight", giftComponent.Weight));
            }

            if (giftComponent.Sugar != 0)
            {
                element.Add(new XElement("Sugar", giftComponent.Sugar));
            }

            if (!string.IsNullOrEmpty(giftComponent.Name))
            {
                element.Add(new XElement("Name", giftComponent.Name));
            }

            if (!string.IsNullOrEmpty(giftComponent.CompanyName))
            {
                element.Add(new XElement("CompanyName", giftComponent.CompanyName));
            }

            element.Add(new XAttribute("MakingType", giftComponent.MakingType));

            if (giftComponent.Wrapper != null)
            {
                element.Add(new XElement("Wrapper",
                                new XAttribute("WrapperType", giftComponent.Wrapper.WrapperType),
                                new XElement("Weight", giftComponent.Wrapper.Weight)));
            }
        }

        private static void AddChocolatePart(IChocolate chocolateComponent, XElement element)
        {
            if (chocolateComponent.Chocolate != 0)
            {
                element.Add(new XElement("Chocolate", chocolateComponent.Chocolate));
            }
        }
    }
}
