
module FlightParser

open ParserUtils

open System.Xml

let airline=  "airline";
let id = "flight_id";
let form = "dom_int";
let arrivalDeparture = "arr_dep";
let airport = "airport";
let scheduleTime = "schedule_time";
let status = "status";
let statusCode = "code";
let statusTime = "time";
let gate = "gate";
let uniqueId ="uniqueID";
let airportName = "name";

type Status = {
    code: string;
    time: string;
}

type Flight = {
    uid: string;
    airline: string
    id: string
    form: string
    arrivalOrDeparture: string;
    destinationAirport: string
    departureAirport: string
    scheduleTime: string;
    status: Status option
    gate: string option;
    
}

let mapStatus (statusAttributes: XmlAttributeCollection option) =
    match statusAttributes with
    | Some attributes ->
       Some({
            code = attributes |> getAttributeValue statusCode;
            time = attributes |> getAttributeValue statusTime;
        })
    | None ->
        None

let mapToFlight airportName (node: XmlNode) : Flight=  
    let arrivalOrDeparture = node |> getNodeText arrivalDeparture |> getText;
    let airport = node |> getNodeText airport |> getText;

    let (destinationAirport, departureAirport) = 
        match arrivalOrDeparture with
        | "A" -> (airportName, airport)
        | _ -> (airport, airportName)
    
    { 
        uid = node |> getAttributes|> getAttributeValue uniqueId
        airline=  node |> getNodeText airline |> getText;
        id = node |> getNodeText id |> getText;
        form = node |> getNodeText form |> getText;
        arrivalOrDeparture = arrivalOrDeparture
        destinationAirport = destinationAirport
        departureAirport = departureAirport
        scheduleTime = node |> getNodeText scheduleTime |> getText;
        status  = node |> getNodeAttributes status|> mapStatus
        gate = node |> getNodeText gate;
    }
    
let parseFlights (xml : string) = 
    let doc = xml |> loadDocument
    let airport = doc.DocumentElement |> getAttributes |> getAttributeValue airportName

    doc.DocumentElement.FirstChild.ChildNodes
    |> Seq.cast<XmlNode>
    |> Seq.map (mapToFlight airport)

