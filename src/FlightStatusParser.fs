
module FlightStatusParser

open ParserUtils

open System.Xml

let code = "code";
let textNorwegian = "statusTextNo"
let textEnglish = "statusTextEn"

type FlightStatus = {
    code: string;
    textEnglish: string;
    textNorwegian: string
}

let mapToFlightStatus (node: XmlNode) : FlightStatus=  
    { 
        code = node |> getAttributes|> getAttributeValue code
        textEnglish =node |> getAttributes|> getAttributeValue textEnglish
        textNorwegian =node |> getAttributes|> getAttributeValue textNorwegian
    }
    
let parseFlightStatuses (xml : string) = 
    let doc = xml |> loadDocument
    doc.DocumentElement.ChildNodes
    |> Seq.cast<XmlNode>
    |> Seq.map mapToFlightStatus

