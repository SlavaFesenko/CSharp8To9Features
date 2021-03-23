namespace CSharp8To9Features.RecordsStuff
{
    public class RecordsLikeLayers
    {
        public static void Show()
        {
            // record-ы гараздо удобнее для создания immutable DTO,
            // но врядли есть много кейсов необходимости перемапливать DAL модель,
            // так как она чаще всего используется в полном объеме
        }

        public static SI GetSiBySitem(SItem sItem)
        {
            return new SI(sItem.Name);
        }

        public static SItem GetSitemBySi(SI sItem)
        {
            return new SItem(sItem.Name);
        }

        public static SIC GetSiBySitem(SItemC sItem)
        {
            return new SIC { Name = sItem.Name };
        }

        public static SItemC GetSitemBySi(SIC sic)
        {
            return new SItemC { Name = sic.Name };
        }
    }

    public record SItem(string Name); // in DAL layer
    public record SI(string Name); // in BizLogic layer

    public class SItemC { public string Name { get; set; } }; // in DAL layer
    public class SIC { public string Name { get; set; } }; // in BizLogic layer
}
