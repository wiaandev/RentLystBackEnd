#!/bin/bash
set -e
set -x

rm -rf ./RentlystBackEnd.Presentation/Migrations || 0

dotnet ef database drop --startup-project=RentlystBackEnd.Presentation/RentlystBackEnd.Presentation.csproj --project=RentlystBackEnd.Domain/RentlystBackEnd.Domain.csproj --force
dotnet ef migrations add --startup-project=RentlystBackEnd.Presentation/RentlystBackEnd.Presentation.csproj --project=RentlystBackEnd.Presentation/RentlystBackEnd.Presentation.csproj InitialCreate
dotnet ef database update --startup-project=RentlystBackEnd.Presentation/RentlystBackEnd.Presentation.csproj --project=RentlystBackEnd.Domain/RentlystBackEnd.Domain.csproj