import React from "react";
import ReactDOM from "react-dom/client";

import App from "./App"

// Crear objeto root | elemento raiz donde se van a reenderizar los componentes

const root = ReactDOM.createRoot(document.getElementById("root"));

root.render(
    <App />
)