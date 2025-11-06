import React, { useEffect, useState } from "react";
import { DragDropContext, Droppable, Draggable } from "@hello-pangea/dnd";

const API_BASE = "http://localhost:5025/api";

export default function App() {
  const [rules, setRules] = useState([]);
  const [error, setError] = useState("");

  useEffect(() => {
    fetch(`${API_BASE}/rules`)
      .then((r) => {
        if (!r.ok) throw new Error("API not reachable");
        return r.json();
      })
      .then((data) => {
        data.sort((a, b) => a.priority - b.priority);
        setRules(data);
      })
      .catch((e) => setError(e.message));
  }, []);

  const reorder = (list, start, end) => {
    const result = Array.from(list);
    const [removed] = result.splice(start, 1);
    result.splice(end, 0, removed);
    return result.map((r, i) => ({ ...r, priority: i + 1 }));
  };

  const onDragEnd = (res) => {
    if (!res.destination) return;
    setRules(reorder(rules, res.source.index, res.destination.index));
  };

  const save = async () => {
    try {
      const payload = {
        rules: rules.map((r) => ({ id: r.id, priority: r.priority })),
      };
      const res = await fetch(`${API_BASE}/rules/reorder`, {
        method: "PUT",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify(payload),
      });
      if (!res.ok) throw new Error("Save failed");
      alert("Saved!");
    } catch (err) {
      alert(err.message);
    }
  };

  return (
    <div style={{ maxWidth: 600, margin: "2rem auto" }}>
      <h2>Rule Prioritization</h2>
      <button onClick={save}>Save changes</button>
      {error && <div style={{ color: "red" }}>{error}</div>}
      <DragDropContext onDragEnd={onDragEnd}>
        <Droppable droppableId="list">
          {(p) => (
            <ul ref={p.innerRef} {...p.droppableProps} style={{ listStyle: "none", padding: 0 }}>
              {rules.map((r, i) => (
                <Draggable key={r.id} draggableId={r.id.toString()} index={i}>
                  {(p) => (
                    <li
                      ref={p.innerRef}
                      {...p.draggableProps}
                      {...p.dragHandleProps}
                      style={{
                        border: "1px solid #ccc",
                        borderRadius: 4,
                        padding: 10,
                        marginBottom: 6,
                        background: "#fff",
                        ...p.draggableProps.style,
                      }}
                    >
                      <strong>{r.priority}. {r.name}</strong>
                    </li>
                  )}
                </Draggable>
              ))}
              {p.placeholder}
            </ul>
          )}
        </Droppable>
      </DragDropContext>
    </div>
  );
}
