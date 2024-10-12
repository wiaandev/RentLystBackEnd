#!/bin/bash
set -e
set -x

rm -rf ./src/BookAPharmacy.Core/Migrations || 0

dotnet ef database drop --startup-project=RentOutBackEnd.Presentation/RentOutBackEnd.Presentation.csproj --project=src/RentOutBackEnd.Domain/RentOutBackEnd.Domain.csproj --force
dotnet ef migrations add --startup-project=src/RentOutBackEnd.Presentation/RentOutBackEnd.Presentation.csproj --project=src/RentOutBackEnd.Domain/RentOutBackEnd.Domain.csproj InitialCreate
dotnet ef database update --startup-project=src/RentOutBackEnd.Presentation/RentOutBackEnd.Presentation.csproj --project=src/RentOutBackEnd.Domain/RentOutBackEnd.Domain.csproj