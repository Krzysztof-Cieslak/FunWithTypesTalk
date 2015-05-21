(**
- title : F# - Fun with types
- description : Introduction to Type First programming
- author : Krzysztof Cieslak
- theme : black
- transition : default

***

### F# - Fun With Types



** Krzysztof Cieslak (@k_cieslak) **

***

### Few words about F#

- Next Generation
- Multi-paradigm
- Cross-platform
- Web scale
- [Buzzword]
- Secure-by-default
- Big data read

---

### Does it run on Mono?

**Yes, it works on Mono, Duo, Trio & Quattro! **

**And supports native hosting on Amazon, Nile and Yangtze**

***

### Functional programming

* Good for mathematical and scientific tasks
* Good for complicated algorithms
* Good for parallel processing

---

** Functional programming is really good for Boring Line of Business Aplications **

---

### Why?

**Typical LoB Application requirements:

* Express requirements clearly
* Rapid development cycle
* High quality
* Fun

***

### Algebraic Data Types

** Algebraic = Composable = LEGO

---
### Tuples

** Finite ordered list of unnamed elements **

*)

(*** hide ***)
type Person = string

(**

*)

type Pair = int * int

type Birthday = Person * System.DateTime

type Foo = int * Pair * Birthday

(**

---

### Record Type

** List of named elements (order doesn't matter)

*)

type Book = {
    Title: string
    Author: Person
    ISBN: string
    }

(**
---
### Union types

* Representing choice
* Each variant can contain different set of data
*)

(*** hide ***)
type CardType = int

(*** hide ***)
type CardNumber = int

(**

*)

type Temperature =
    | F of int
    | C of float

type PaymentMethod =
    | Cash
    | Checque of int
    | Card of CardType * CardNumber

(**
---
### Example 1 - Card Game Domain

*)

module CardGameBoundedContext =
    type Suit = Club | Diamond | Spade | Heart

    type Rank = Two | Three | Four | Five | Six | Seven | Eight
                | Nine | Ten | Jack | Queen | King | Ace

    type Card = Suit * Rank

    type Hand = Card list
    type Deck = Card list

    type Player = { Name: string; Hand: Hand }
    type Game = { Deck: Deck; Players: Player list}

    type Deal = Deck -> (Deck * Card) // X -> Y means a function
                                      // input of type X
                                      // output of type Y

    type PickupCard = (Hand * Card) -> Hand

(**

***

### DDD in F#

* Value Type - don't have identity, we compare them using all properties, should be immutable
* Entity objects - have identity, we compare them using identity key, can change state during lifespan

---
### Value Type in F#

*)

type PostalAdress = {
    Street: string
    City: string
    PostalCode: string
}

(**

---
### Entity Type in F#

 *)

[<CustomEquality; NoComparison>]
type Order = { Id :int; Adress : PostalAdress } with
    override this.GetHashCode() = hash this.Id

    override this.Equals(other) =
        match other with
        | :? Order as o -> this.Id = o.Id
        | _ -> false

(**

---
### Even better Entity Type in F#


*)

[<NoEquality; NoComparison>]
type BetterOrder = { Id :int; Adress : PostalAdress }

(**

---
### Option types I

*)

type lengthFunction = string -> int

let length : lengthFunction = fun s -> s.Length

(**

---
### Option type II

*)

type Option<'T> =
    | Some of 'T
    | None

type betterLengthFunction = string -> Option<int>

let betterLength : betterLengthFunction =
    fun s -> match s with
             | null -> None
             | _ -> Some s.Length

(**
---
### Option type III

** Good news, option type is core F# type. **

*)

type evenBetterLengthFunction = string -> int option

(**
---
### Fighting with "Prmitive Obsession"

* Extractig one field concepts to seperate types


*)

type Email = Email of string
type OrderId = OrderId of int
type CustomerId = CustomerId of int

(**

***

### Example 2 - Package delivery

*)


type Shipment = {
    Id : int
    PackageName : string
    PackageWeight : float
    TruckId : int
    SentDate : System.DateTime
    DeliveredDate : System.DateTime
    Confirmation : string
    Desription : string
}

(**

---

*)
module Example2 =

    type Confirmation = SignatureHash of string
    type ShipmentId = ShipmentId of int
    type TruckId = TruckId of int
    type Weight = Weight of float

    type Package = {
        Name : string
        Weight: Weight
        Description : string option
    }

    type Shipment = {
        Id: ShipmentId
        Package : Package
        SentDate : System.DateTime
        TruckId : TruckId
        DeliveredDate : System.DateTime
        Confirmation : Confirmation
    }

(**

---

*)

module Example3 =

    type Confirmation = SignatureHash of string
    type ShipmentId = ShipmentId of int
    type TruckId = TruckId of int
    type Weight = Weight of float
    type SentDate = System.DateTime
    type DeliveredDate = System.DateTime

    type Package = {
        Name : string
        Weight: Weight
        Description : string option
    }

    type UndeliveredShipment = ShipmentId * Package
    type OutForDeliveryShipment = ShipmentId * Package * TruckId * SentDate
    type DeliveredShipment = ShipmentId * Package * DeliveredDate * Confirmation


(**

---

*)

module Example4 =

    type Confirmation = SignatureHash of string
    type ShipmentId = ShipmentId of int
    type TruckId = TruckId of int
    type Weight = Weight of float
    type SentDate = System.DateTime
    type DeliveredDate = System.DateTime

    type Package = {
        Name : string
        Weight: Weight
        Description : string option
    }

    type UndeliveredData = ShipmentId * Package
    type OutForDeliveryData = ShipmentId * Package * TruckId * SentDate
    type DeliveredData = ShipmentId * Package * DeliveredDate * Confirmation

    type Shipment =
        | UndeliveredState of UndeliveredData
        | OutForDeliveryState of OutForDeliveryData
        | DeliveredState of DeliveredData

(**
***
### Links

* http://fsharp.org/
* http://fpchat.com/
* http://dungpa.github.io/fsharp-cheatsheet/
* http://fsharpforfunandprofit.com/
* http://fsprojects.github.io/
* http://tomasp.net/

*)
