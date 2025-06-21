#!/bin/bash
set -e
set -x

rm -rf ./RentroBackEnd.Presentation/Migrations || 0

dotnet ef database drop --startup-project=RentroBackEnd.Presentation/RentroBackEnd.Presentation.csproj --project=RentroBackEnd.Domain/RentroBackEnd.Domain.csproj --force
dotnet ef migrations add --startup-project=RentroBackEnd.Presentation/RentroBackEnd.Presentation.csproj --project=RentroBackEnd.Presentation/RentroBackEnd.Presentation.csproj InitialCreate
dotnet ef database update --startup-project=RentroBackEnd.Presentation/RentroBackEnd.Presentation.csproj --project=RentroBackEnd.Domain/RentroBackEnd.Domain.csproj