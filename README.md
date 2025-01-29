# Hunter Shooting Game

This is a 2D shooting game developed using MonoGame. The game features a hunter shooting vertically at birds. The project includes both a desktop version and an Android version.

## Prerequisites

- [.NET SDK](https://dotnet.microsoft.com/download) (version 8.0 or later)
- [MonoGame](http://www.monogame.net/downloads/)
- [Visual Studio](https://visualstudio.microsoft.com/) or [Visual Studio Code](https://code.visualstudio.com/)

## Project Structure
HunterShootingMonogame/ ├── Android/ │ ├── .config/ │ │ └── dotnet-tools.json │ ├── Activity1.cs │ ├── AndroidManifest.xml │ ├── Bird.cs │ ├── Bullet.cs │ ├── Content/ │ │ ├── arial.spritefont │ │ ├── Content.mgcb │ │ └── ... │ ├── Fireball.cs │ ├── Game1.cs │ ├── Resources/ │ ├── ShootingGameAndroidNew.csproj │ └── obj/ │ └── ... ├── Desktop/ │ ├── .config/ │ ├── Bird.cs │ ├── Bullet.cs │ ├── Content/ │ │ ├── arial.spritefont │ │ ├── Content.mgcb │ │ └── ... │ ├── Fireball.cs │ ├── Game1.cs │ ├── Program.cs │ ├── ShootingGame.csproj │ └── obj/ │ └── ..

## Building and Running the Game

### Desktop Version

1. **Navigate to the Desktop directory:**

    ```sh
    cd Desktop
    ```

2. **Restore .NET tools and dependencies:**

    ```sh
    dotnet tool restore
    dotnet restore
    ```

3. **Build the project:**

    ```sh
    dotnet build
    ```

4. **Run the game:**

    ```sh
    dotnet run
    ```

### Android Version

1. **Navigate to the Android directory:**

    ```sh
    cd Android
    ```

2. **Restore .NET tools and dependencies:**

    ```sh
    dotnet tool restore
    dotnet restore
    ```

3. **Build the project:**

    ```sh
    dotnet build
    ```

4. **Deploy and run the game on an Android device or emulator:**

    ```sh
    dotnet run
    ```

## Game Controls

- **Desktop:**
  - Use the mouse to aim and click to shoot.
  
- **Android:**
  - Tap on the screen to shoot.

## Technical Details

- The game is built using the MonoGame framework.
- The project targets .NET 8.0.
- The game assets are located in the `Content` directory of each project.
- The main game logic is implemented in `Game1.cs`.

## Contributing

Contributions are welcome! Please fork the repository and submit pull requests.

## License

This project is licensed under the MIT License. See the LICENSE file for details.

---

Enjoy the game! If you encounter any issues, feel free to open an issue on GitHub.