
import "bootstrap/dist/css/bootstrap.min.css"
import { useEffect, useState } from "react";

const App = () => {

    const [tareas, setTareas] = useState([]);
    const [codigo, setCodigo] = useState("");
    const [descripcion, setDescripcion] = useState("");


    const mostrarTareas = async () => {
        const response = await fetch("api/tarea/Listar");
        if (response.ok) {
            const data = await response.json();
            setTareas(data);
            console.log(data);
        } else {
            console.log("StatusCode: " + response.status);
        }
    }

    //3.- Función para Convertir Fecha
    const formatDate = (str) => {
        let options = { year: 'numeric', month: 'long', day: 'numeric' };
        let fecha = new Date(str).toLocaleDateString("es-NI", options);
        let hora = new Date(str).toLocaleTimeString();
        return fecha + ' | ' + hora;
    }

    useEffect(() => {
        mostrarTareas();
    }, [])

    // Crear tareas
    const guardarTarea = async (e) => {
        //console.log('Guardar Tarea');

        e.preventDefault();

        let req = {};
        req.codigo = codigo;
        req.descrpcion = descripcion;
        req.estadoId = 1;

        //console.log(req);

        const response = await fetch("api/tarea/Guardar", {
            method: "POST",
            headers: {
                'Content-Type': 'application/json;charset=utf-8'
            },
            body: JSON.stringify(req)
            //body: JSON.stringify({ codigo: codigo, descripcion: descripcion })
        });
        if (response.ok) {
            setCodigo("");
            setDescripcion("");
            await mostrarTareas();
        }
    }

    // Cerrar tareas
    const cerrarTarea = async (id) => {

        const response = await fetch(`api/tarea/Cerrar/${id}`, {
            method: "DELETE"
        });
        if (response.ok) {
            await mostrarTareas();
        }
    }

    return (

        <div className="container bg-dark p-4 vh-100">
            <h2 className="text-white"> Lista de Tareas </h2>
            <div className="row">
                <div className="col-sm-12">

                    <form onSubmit={guardarTarea}>
                        <div className="input-group">
                            <input type="text"
                                className="form-control"
                                placeholder="Ingresar Codigo de la Tarea"
                                value={codigo}
                                onChange={(e) => setCodigo(e.target.value)}
                            >
                            </input>
                            <input type="text"
                                className="form-control"
                                placeholder="Ingresar Descripcion de la Tarea"
                                value={descripcion}
                                onChange={(e) => setDescripcion(e.target.value)}
                            >
                            </input>
                            <button type="submit" className="btn btn-success">Agregar Tarea</button>
                        </div>
                    </form>

                </div>
            </div>

            <div className="row mt-4">
                <div className="col-sm-12">

                    <div className="list-group">
                        {
                            tareas.map(
                                (item) => (
                                    <div key={item.id} className="list-group-item list-group-item-action">
                                        <h6 className="text-warning">{item.codigo}</h6>
                                        <h5 className="text-primary">{item.descrpcion}</h5>

                                        <div className="d-flex justify-content-between">

                                            <small className="text-muted">{formatDate(item.fechaRegistro)}</small>
                                            <button className="btn btn-sm btn-outline-danger"
                                                onClick={() => cerrarTarea(item.id)}
                                            >
                                                Cerrar
                                            </button>

                                        </div>
                                    </div>
                                )
                            )
                        }
                    </div>

                </div>
            </div>
        </div>

    )
}

export default App;