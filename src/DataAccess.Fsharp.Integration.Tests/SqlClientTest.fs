module DataAccess.Fsharp.Integration.Tests

open NUnit.Framework
open System

[<Test>]
let AddPlayer_NewPalyer_PlayerAdded () =

    // Arrange
    let personId = 1;

    // Act
    SqlClient.AddPlayer.execute personId true true

    // Assert
    let result = SqlClient.GetPlayerBaseInfo.execute personId
    Assert.True(result.IsSome)

[<Test>]
let GetPlayerBaseInfo_ForPlayer_PlayerExists () =
    
    // Arrange
    let playerId = 1

    // Act
    let result = SqlClient.GetPlayerBaseInfo.execute playerId
    
    // Assert
    Assert.True(result.IsSome)
    printfn "%A" result

[<TestCase(2, null)>]
[<TestCase(3, 2)>]
[<TestCase(null, 3)>]
let SetPlayerCoach_NewPalyer_CoachSet (newCoachIdParam:Nullable<int>) (prevCoachIdParam:Nullable<int>) =
    
    // Arrange
    let playerId = 1
    let newCoachId = if newCoachIdParam.HasValue then Some newCoachIdParam.Value else None
    let prevCoachId = if prevCoachIdParam.HasValue then Some prevCoachIdParam.Value else None

    // Act
    SqlClient.SetPlayerCoach.execute playerId newCoachId prevCoachId

    // Assert
    let result = SqlClient.GetPlayerBaseInfo.execute playerId
    Assert.True(result.IsSome)

[<Test>]
let SetPlayerCoach_OldState_DbUpdateConcurrencyException () =
    
    // Arrange
    let playerId = 1
    let coachId = Some 1000
    let previousCoachId = Some 999

    // Act
    let result = fun () -> SqlClient.SetPlayerCoach.execute playerId coachId previousCoachId |> ignore

    // Assert
    Assert.Throws<SqlClient.SetPlayerCoach.DbUpdateConcurrencyException>(TestDelegate result) |> ignore;