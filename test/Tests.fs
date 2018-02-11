namespace Test

open Microsoft.VisualStudio.TestTools.UnitTesting

open AirportParser
open AirlineParser
open FlightStatusParser
open FlightParser

[<TestClass>]
type TestClass () =

    [<TestMethod>]
    member this.AirportsParserTest () =
        let airports = System.IO.File.ReadAllText("../../../airports.xml") |> parseAirports
        let selectedAirport =  airports |> Seq.head
        
        Assert.AreEqual(2010, airports |> Seq.length);
        Assert.AreEqual("AAH", selectedAirport.code);
        Assert.AreEqual("Aachen", selectedAirport.name);       

    [<TestMethod>]
    member this.AirlinesParserTest () =
        let airlines = System.IO.File.ReadAllText("../../../airlines.xml") |> parseAirlines |> Seq.toArray
        let selectedAirline =  airlines.[10]
        
        Assert.AreEqual(816, airlines |> Seq.length);
        Assert.AreEqual("AEG", selectedAirline.code);
        Assert.AreEqual("AirEst", selectedAirline.name);       

    
    
    [<TestMethod>]
    member this.FlightStatusesParserTest () =
        let flightStatuses = System.IO.File.ReadAllText("../../../flightstatuses.xml") |> parseFlightStatuses |> Seq.toArray
        let selectedFlightStatus =  flightStatuses.[3]
        
        Assert.AreEqual(5, flightStatuses |> Seq.length);
        Assert.AreEqual("A", selectedFlightStatus.code);
        Assert.AreEqual("Arrived", selectedFlightStatus.textEnglish);
        Assert.AreEqual("Landet", selectedFlightStatus.textNorwegian);

    [<TestMethod>]
    member this.FlightsParserTest () =
        let flights = System.IO.File.ReadAllText("../../../flights.xml") |> parseFlights |> Seq.toArray
        let selectedFlight =  flights.[0]
        
        Assert.AreEqual(9, flights |> Seq.length);
        Assert.AreEqual("7335217", selectedFlight.uid);
        Assert.AreEqual("WF", selectedFlight.airline);
        Assert.AreEqual("WF591", selectedFlight.id);
        Assert.AreEqual("D", selectedFlight.form);
        Assert.AreEqual("2018-02-09T16:35:00Z", selectedFlight.scheduleTime);
        Assert.AreEqual("BGO", selectedFlight.departureAirport);
        Assert.AreEqual("KRS", selectedFlight.destinationAirport);
        Assert.IsNotNull(selectedFlight.status);
        Assert.AreEqual("E", selectedFlight.status.Value.code);
        Assert.AreEqual("2018-02-09T16:40:00Z", selectedFlight.status.Value.time);
        Assert.IsTrue(selectedFlight.gate.IsNone);