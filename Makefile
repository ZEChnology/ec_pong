build pong.exe:
	mcs -out:pong.exe -pkg:dotnet pong/*.cs

run: pong.exe
	mono pong.exe

test:
	nunit-console -nologo pong.Tests/pong.Tests.csproj

clean:
	rm -f *.exe *.dll

.PHONY: clean run build all
