# ColorBlast

## Overview

This project is a puzzle game developed in Unity, where players match and clear pieces on a game board. The game features various game modes, board sizes, and special pieces that introduce unique mechanics and challenges.

## Main Components

1. **GameManager:**
   - Responsible for managing the game flow, including starting the game, handling user input, and transitioning between UI pages.

2. **Board:**
   - Represents the game board grid, consisting of individual tiles where pieces can be placed.
   - Manages the arrangement and behavior of tiles during gameplay, including refilling empty spaces and detecting matches.

3. **Tile:**
   - Represents individual cells within the game board grid.
   - Stores information such as position, adjacent tiles, and the piece occupying the tile.

4. **PieceObject:**
   - Represents game pieces that occupy tiles on the game board.
   - Contains logic for initializing, animating, and interacting with pieces, including special effects.

5. **MainPageUI:**
   - UI page responsible for displaying the main menu and allowing users to select game options such as board size and fill type.

6. **PlayPageUI:**
   - UI page displayed during gameplay, showing the game board and relevant information such as score and level progress.

## Key Features

1. **GenerateBoard:**
   - Utility method for generating the game board based on specified parameters such as size, tile prefab, and game settings.
   - Instantiates tiles, positions them within the grid, and initializes their properties.

2. **Fill:**
   - Mechanism for filling empty spaces on the game board after pieces are removed or special effects are triggered.
   - Utilizes different strategies (e.g., falling pieces) to refill the board in a visually appealing manner.

3. **Solve:**
   - Functionality for identifying and resolving matches or combinations of pieces on the game board.
   - Detects patterns or configurations of pieces that meet specific criteria and triggers corresponding actions (e.g., removing pieces, activating special effects).

4. **Screen Responsive:**
   - `SceenResponsive` utility class ensures that the game's display adjusts dynamically to different screen sizes and aspect ratios.
   - Automatically adjusts the camera's orthographic size to maintain consistent visuals and gameplay experience across various devices.

## Extensible

The project architecture and design patterns are designed to be highly extensible, allowing for easy addition of new features, game modes, and mechanics. Some aspects that can be extended or modified include:

- **New Piece Types:** Easily add new types of pieces with unique behaviors and effects by creating new subclasses of `PieceObject` and implementing custom logic.
  
- **Special Effects:** Extend the game's capabilities by introducing new special effects or power-ups that interact with pieces and the game board. Implement new solver classes to handle these effects and modify the `Fill` method to accommodate them.
  
- **Game Modes:** Expand the game with different game modes, such as timed challenges, objective-based levels, or endless modes. Customize the `GameManager` to support these modes and adjust game rules and mechanics accordingly.
  
- **UI Enhancements:** Improve the user interface by adding new UI elements, animations, or visual feedback to enhance the player experience. Modify the existing UI scripts or create new ones to implement these enhancements.
  
- **Customization Options:** Allow players to customize game settings, such as board size, piece types, or difficulty levels. Implement UI controls and backend logic to support these customization options and provide a more personalized gameplay experience.
