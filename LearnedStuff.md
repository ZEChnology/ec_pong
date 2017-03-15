# Stuff I Learned

## C#

### Building
The default template from `grunt-init csharpsolution` defines a `csproj` file to build with `<OutputType>Library</OutputType>`.  This means that build the project will result in a `dll` file, which for this game wasn't going to cut it.  By simply removing that line, you get the default build output type, which is to make an `exe`.  Hooray!

### Files
I started off naming stuff in lowercase as I'm used to, but learned that it is customary to name things starting with a capital.  `Ball.cs` instead of `ball.cs`.  It is generally accepted that one class per file is good.  More important than either of these is that you need to include each new file in your `csproj` file which, via the `sln` file is how the compiler knows what to build. The files get their own `<ItemGroup>`

```
  <ItemGroup>
    <Compile Include="Properties/AssemblyInfo.cs" />
    <Compile Include="Ball.cs" />
    <Compile Include="Paddle.cs" />
    <Compile Include="Pong.cs" />
    <Compile Include="Board.cs" />
    <Compile Include="GameObject.cs" />
  </ItemGroup>
```

### References
All the `using Whatever.Something;` that you find at the top of files are references.  They give you easy access to the things that are in that namespace.  You don't *have* to use them.  You could specify the fully qualified name for every object you use, but that would suck.  The compiler needs to know about them though, so any reference you're adding needs to get put in the `csproj` file in an `<ItemGroup>` 

```
  <ItemGroup>
    <Reference Include="System" />
    <Reference Include="System.Core" />
    <Reference Include="System.Drawing" />
    <Reference Include="System.Windows.Forms" />
    <Reference Include="System.Data" />
  </ItemGroup>
```

### Linked Resources
I ran into file path issues when running my tests and [linked resources](https://msdn.microsoft.com/en-us/library/ht9h2dk8(v=vs.100).aspx) were the answer.  They allow you to take any old file, in this case some PNGs, and have them referencable by name.  As far as I can tell, this just means it will name the resource what you want and move the file to build directory to make it accessible.  In the code, that means something like this...

```
            var assembly = System.Reflection.Assembly.GetExecutingAssembly();
            var stream = assembly.GetManifestResourceStream("pong.Resources.ball.png");
            _sprite = new Bitmap(stream);
```

instead of this...
```
            _sprite = new Bitmap("some/file/path/ball.png");
```

Although the first is more verbose, you can be guaranteed that the resource will be available to you without specifying a file path.  I set my resources up in my `csproj` file with another (you guessed it) `<ItemGroup>`

```
  <ItemGroup>
    <EmbeddedResource Include="../pong/Resources/ball.png">
      <Link>Resources/ball.png</Link>
    </EmbeddedResource>
    <EmbeddedResource Include="../pong/Resources/paddle.png">
      <Link>Resources/paddle.png</Link>
    </EmbeddedResource>
  </ItemGroup>
```

### Interfaces
[Interfaces](https://msdn.microsoft.com/en-us/library/87d83y5b.aspx) are awesome.  In short, they are a way to define a public interface (method signatures, attributes, properties) that you can apply to any other class and have that interface inforced.  The compiler will yell loudly if you fail in implementing the interface.  But the real power of the inteface comes in using it in place of a class when defining a method.  If you are defining your functions to take things that implement an interface, you are allow the implementation(classes) to change without needing to touch the things that consume objects that use that interface.  The other, perhaps more vital way they are awesome, is that they make testing stuff easier.  Letting your methods and functions take anything that implements a particular interface means that you can create mocks that implement the interface and pass them in to make assertions about things that are called and whatnot.  You simply *cannot* do this with classes.


## Project Setup

### grunt-init csharpsolution
This is pretty nice.  It sets up the skeleton of a project with a `csproj` files and a `sln` as well as a couple test deps.  The template is easily modified or extended, and it doesn't do *too* much, so I give it two thumbs up.  I did update the file paths in the templated `csproj` and `sln` files to be unix style instead of windows, although other anecdotal evidence suggests that this wasn't necessary.  Will experiment to find out.


## Omnisharp

Wow, What A Pain.  I spent really too long trying to get the roslyn server built on my machine to no meaningful end.  Just when I got past a terrible open ssl issue which they talk about shortly [here](https://github.com/dotnet/core/blob/c6d3b21e9358581dae26ed67d96833c1b0db43fd/cli/known-issues.md#openssl-dependency-on-os-x) as if these instructions actually solved anybody's problem, I ran into issue in the Test phase of the build.  Some missing test library dlls.  No solution on the interwebs worked for me though.  So I finally gave up and just decided to use the OLD server.

The old server is great!  I had to modify the server's `config.json` to swap the test command from using `nunit-console.exe` to just `nunit-console`.  Once done, I was able to build projects and run tests from within vim.  I was able to do a little python debugging (parts of the server are in python) to determine the build command it was using to create the executables and libraries from the project.  Turns out they are pretty straightforward too. Take a look at the `Makefile` to see for yourself.  Why anybody would need VisualStudio to do that for them I don't know.  So much so that few people on the internet can offer up such a straightforward command.  "Just use VS". It saddens me so much. 

The most useful part for me so far is that it will tell me when I build that I'm missing a reference to something, and there's a command to add that reference.  Nice!


## Dependencies

### Nuget
My mono installation also gave me the `nuget` binary, and it is how you install your stuff. With a `nuget restore` and a `yourproject.sln` file in the same directory, it will pull the dependencies from the `.csproj` files for `yourproject` and `yourproject.Test` and drop them into a `packages` directory. Neat.


## Tests

The thing that really gave me pains when starting to write my tests was that the path of execution on the file system was different than the path for just running the `pong.exe`, which meant that my file paths for my PNGs were wrong.  I didn't want to hardcode absolute paths, which led me down the long and winding road to Linked Resources as explained above.  Once I was past this, there were really only two things that I needed to figure out...

### Mocks
[NSubstitute](http://nsubstitute.github.io/) appears to be a widely used mocking library for C#.  As [stated](http://nsubstitute.github.io/help/creating-a-substitute/) in their docs, mocking a class is dangerous, and apparently in practice in really really hard to do in C#.  This is because of the type system.  When your code says that a function takes a `Rectangle` for an arg, it's going to be pissed that it got an `NSubstitute.whatever` instead.  C#'s way around this problem is to mock interfaces.  Because you can write your function to take *anything* that implements a particular interface, you can simply send in your mock object that also happens to impelement that interface. This led me down the path of interfaces. 

### Setup
Very straightforward with just adding this little ditty to the top of your test class.

```
        [SetUp]
        public void Init()
        {
          ...
        }
```
