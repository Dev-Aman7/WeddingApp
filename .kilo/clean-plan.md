# Wedding App Clean Plan

## Backend (.NET)
1. Rename `WeddingApp.Controller` namespace to `WeddingApp.Controllers`
2. Make `AuthController.Login` async with `FirstOrDefaultAsync`
3. Extract DTOs (`LoginRequest`, `SignUpRequest`, `TodoDto`) to `WeddingApp/DTOs/`
4. Mark `Guest.Name` as `required`
5. Update vulnerable JWT-related NuGet packages

## Frontend (React/Vite)
1. Fix `PageShell.jsx` prop-types lint error
2. Fix `vite.config.js` proxy target to correct API port
3. Update `index.html` title
4. Add missing `signup` and `todos` CRUD endpoints to `apiSlice.js`

## Project
1. Add root `.gitignore`
