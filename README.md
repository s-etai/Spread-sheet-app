# Spreadsheet App

A C# Windows Forms spreadsheet application developed for my **Object-Oriented Software Principles** course.  

This project demonstrates core concepts in **object-oriented programming**, design patterns, and software architecture in a practical application.

---

## Features

- Supports basic spreadsheet functionality: cell editing, formulas, expression evaluation, and cell references.
- Implements **Undo/Redo** functionality using the **Command pattern**.
- Formula parsing with an **expression tree** structure.
- Event-driven updates using **Observer pattern** (`INotifyPropertyChanged`) to keep the UI synchronized with underlying data.
- Clean **layered architecture** separating:
  - **Presentation Layer (UI)**: Windows Forms and DataGridView for user interaction.
  - **Spreadsheet engine Layer**: Spreadsheet and Cell classes for managing data and formula evaluation
- Designed with **high cohesion** and **low coupling** for maintainability and extensibility.

---

## Demo Video

Watch the spreadsheet in action: [YouTube Demo](https://www.youtube.com/watch?v=8VdRVoDOk1Y)

---

## Technologies Used

- C#  
- .NET Framework / Windows Forms  
