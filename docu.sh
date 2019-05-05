dotnet tool install -g Wyam.Tool
mkdir docu
cd docu
wyam new --recipe Docs
wyam -r Docs
