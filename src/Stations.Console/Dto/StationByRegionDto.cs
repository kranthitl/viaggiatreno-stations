namespace Stations.Console.Dto;

public record StationByRegionDto(
    int CodReg,
    int TipoStazione,
    DettZoomStaz[] DettZoomStaz,
    // object[] Pstaz,
    MappaCitta MappaCitta,
    string CodiceStazione,
    string CodStazione,
    double Lat,
    double Lon,
    double LatMappaCitta,
    double LonMappaCitta,
    Localita Localita,
    bool Esterno,
    int OffsetX,
    int OffsetY,
    string NomeCitta
);

public record DettZoomStaz(
    string CodiceStazione,
    int ZoomStartRange,
    int ZoomStopRange,
    bool PinpointVisibile,
    bool PinpointVisible,
    bool LabelVisibile,
    bool LabelVisible,
    object CodiceRegione
);

public record MappaCitta(
    string UrlImagePinpoint,
    string UrlImageBaloon
);

public record Localita(
    string NomeLungo,
    string NomeBreve,
    string Label,
    string Id
);

