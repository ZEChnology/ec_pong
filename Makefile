deps:
	nuget restore

build pong/obj/Debug/pong.exe pong.Tests/obj/Debug/pong.Tests.dll:
	xbuild /nologo /v:q /property:GenerateFullPaths=true pong.sln

run: pong/obj/Debug/pong.exe
	mono pong/obj/Debug/pong.exe

test: pong.Tests/obj/Debug/pong.Tests.dll 
	nunit-console -nologo pong.Tests/pong.Tests.csproj

clean:
	rm -rf pong/obj pong/bin pong.Tests/obj pong.Tests/bin

.PHONY: clean run build test deps
