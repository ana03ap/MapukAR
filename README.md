# 🏺 Mapukar - Realidad Aumentada para el Museo Mapuka

**Mapukar** es una aplicación de realidad aumentada e interacción digital desarrollada para enriquecer la experiencia del visitante en el Museo Mapuka de la **Universidad del Norte**.

> 📍 **Importante:** Esta aplicación no está disponible para descarga pública. Solo puede utilizarse en las **tablets oficiales ubicadas dentro del museo**, ya que su funcionalidad está diseñada para integrarse con los espacios, marcadores y modelos físicos del recorrido expositivo.

---

## 🎯 Objetivo del Proyecto

Integrar tecnología de realidad aumentada y juegos interactivos como herramientas educativas dentro del museo, permitiendo al visitante:

- Visualizar piezas arqueológicas y animales prehistóricos en 3D con RA.
- Interactuar con minijuegos para descubrir conocimientos de manera lúdica.
- Seguir un flujo narrativo seccionado por temáticas, vinculado al recorrido real.

---

## 🧩 Características Principales

| Módulo                    | Descripción |
|---------------------------|-------------|
| 🧠 Secciones y navegación | Control de flujo por `SectionFlowManager`, que gestiona qué sección se muestra. |
| 🖼️ Canvases interactivos  | Cada sección tiene subpantallas gestionadas por `CanvasFlowManager`. |
| 🧱 Minijuegos             | Juegos de interacción como rompecabezas y simulación de encendido de fuego. |
| 📱 Realidad Aumentada     | Colocación, escala y movimiento de modelos 3D de animales y artefactos. |

---

## 📂 Estructura del Proyecto

```
Assets/
├── 3DModels/                # Modelos de animales y artefactos arqueológicos
├── Scripts/                 # Controladores de secciones, AR, juegos y UI
├── Scenes/                  # Escena principal del recorrido
├── Plugins/, XR/, Prefabs/  # Dependencias y modelos reutilizables
├── S1-image/ a S8-image/    # Recursos por sección
```

---

## 🎮 Ejemplos de Experiencias

### 🧩 Rompecabezas Interactivo
El visitante debe arrastrar piezas de un objeto patrimonial hasta formar el modelo completo. Al completar, se activa una animación o modelo 3D.

### 🔥 Juego del Fuego
Tocando la pantalla repetidamente, el usuario enciende una fogata como lo hacían los pueblos ancestrales. Aparece un botón final al lograrlo.

### 🦕 Realidad Aumentada
Modelos como el Glyptodon, Megalania, Mamut y aves esculpidas se posicionan frente al usuario. Pueden moverse y girar con gestos simples.

---

## 🛠️ Tecnologías Utilizadas

- Unity 2022.3+
- C# (MonoBehaviour)
- AR Foundation (con ARKit y ARCore)
- Canvas clásico de Unity (UI Toolkit)
- Input System (para interacción táctil)
- Formato OBJ y PNG para modelos 3D

---

## 🔐 Disponibilidad

Esta aplicación está instalada exclusivamente en **tablets ubicadas en estaciones interactivas del Museo Mapuka**. No tiene versión pública ni remota.

---

## 🤝 Colaboraciones

Este proyecto está abierto a colaboraciones. Si deseas contribuir, por favor:

- Crea un issue describiendo tu propuesta o funcionalidad.
- Haz un fork del repositorio y envía tu pull request con tus cambios.

¡Todas las contribuciones y sugerencias son bienvenidas!

---

## 🚀 Roadmap

- Agregar nuevas funcionalidades a cada sección
- Aplicar nuevas animaciones
- Implementar nuevas interacciones
- Corregir bugs de los modelos 3D
- Agregar más modelos 3D
- Mejorar la responsividad para diferentes tamaños de pantalla (actualmente solo funciona para la tablet del museo)

---

## 👨‍🎓 Créditos

Desarrollado por estudiantes de Ingeniería de Sistemas  
**Universidad del Norte - 2025-01**  
Aplicación presentada como proyecto final  
- Ana Ardila  
- Darwin Charris  
- Emily Roldán

---

## 📜 Licencia y Uso

Este software es de uso interno para fines culturales, educativos y de divulgación científica. Todos los modelos, imágenes y animaciones están protegidos por los derechos del museo y la universidad.
