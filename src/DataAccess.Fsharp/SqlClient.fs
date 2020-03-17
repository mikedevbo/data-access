module SqlClient

    open FSharp.Data
    
    [<Literal>]
    let private connectionStringName = "name=Players"

    module AddPlayer =
        [<Literal>]
        let private sql = "
            insert into dbo.Player(id, IsRightHanded, IsTwoHandedBackhand, CoachId)
            values(@Id, @IsRightHanded, @IsTwoHandedBackhand, null)
            "
        let execute personId isRightHanded isTwoHandedBackhand =
            use cmd = new SqlCommandProvider<sql, connectionStringName>()
            cmd.Execute(personId, isRightHanded, isTwoHandedBackhand) |> ignore
            ()

    module GetPlayerBaseInfo =
        [<Literal>]
        let private sql = "
            select
                pbi.id
                , pbi.FirstName
                , pbi.LastName
                , pbi.BirthYear
                , pbi.BirthMonth
                , pbi.BirthDay
                , pbi.BirthplaceCountry
                , pbi.BirthplaceCity
                , pbi.[Weight]
                , pbi.Height
                , pbi.IsRightHanded
                , pbi.IsTwoHandedBackhand
                , pbi.CoachId
                , pbi.CoachFirstName
                , pbi.CoachLastName
            from
                dbo.PlayersBaseInfo pbi
            where
                pbi.Id = @Id
            "
        let execute playerId =
            use cmd = new SqlCommandProvider<sql, connectionStringName, SingleRow = true>()
            cmd.Execute(Id = playerId)


    module SetPlayerCoach =
    
        exception DbUpdateConcurrencyException

        [<Literal>]
        let private sql = "
            declare @prevCoachId int = @prevCoachIdVal
            update
                dbo.Player
            set
                CoachId = @newCoachId
            where
                Id = @Id
                and ((@prevCoachId is null and CoachId is null) or (@prevCoachId = CoachId))
            "
        let execute (playerId:int) (newCoachId:Option<int>) (prevCoachId: Option<int>) =
            use cmd = new SqlCommandProvider<sql, connectionStringName, AllParametersOptional = true>()
            let result = cmd.Execute(prevCoachId, newCoachId, (Some playerId))
            if result = 0 then raise (DbUpdateConcurrencyException)