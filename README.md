# ProyectoMetodos

# Documentación del Proyecto

## Archivos

### `matrix.py`
Este archivo contiene la implementación de una clase `Matrix` que permite trabajar con matrices de manera eficiente. Incluye métodos para realizar operaciones comunes como determinantes, transposición, adjunta, inversa, multiplicación, escalado, y más. También soporta operaciones avanzadas como la expansión por cofactores y el método de Sarrus para calcular determinantes.

#### Principales características:
- **Clase `Matrix`:**
    - Propiedades:
        - `Matrix`: Obtiene o establece la matriz.
        - `is_square`: Verifica si la matriz es cuadrada.
        - `n_rows`, `n_cols`: Obtiene el número de filas y columnas.
        - `det`: Calcula el determinante de la matriz.
        - `T`: Devuelve la transpuesta de la matriz.
        - `adj`: Calcula la matriz adjunta.
        - `inv`: Calcula la matriz inversa.
        - `flat`: Devuelve una lista con los elementos de la matriz en orden plano.
    - Métodos:
        - `get_item`, `set_item`: Obtiene o establece un elemento específico.
        - `get_row`, `get_col`: Obtiene una fila o columna específica.
        - `set_row`, `set_col`: Establece una fila o columna específica.
        - `scale_item`, `scale_row`, `scale_col`: Escala un elemento, fila o columna por un escalar.
        - `divide_matrix`: Divide la matriz en submatrices excluyendo filas o columnas específicas.
        - `dot`: Realiza el producto punto entre matrices.
        - Métodos estáticos:
            - `_expansion_cofactors`: Calcula el determinante usando expansión por cofactores.
            - `_sarrus`: Calcula el determinante usando el método de Sarrus para matrices de 3x3 o menores.
    - Sobrecarga de operadores:
        - `__mul__`: Multiplicación por escalar o por otra matriz.
        - `__truediv__`: División por escalar.
        - `__repr__`: Representación en cadena de la matriz.

---

### `functions.py`
Este archivo contiene funciones auxiliares para trabajar con cadenas de texto y matrices. Incluye métodos para convertir cadenas en matrices y viceversa, así como para generar matrices a partir de claves hash.

#### Principales funciones:
- **`num_2_str(n: int) -> str`:**
    Convierte un número entero en un carácter basado en un conjunto predefinido de caracteres. Si el número no es válido, devuelve un carácter desconocido (`░`).

- **`str_2_num(s: str) -> int`:**
    Convierte un carácter en su índice numérico dentro de un conjunto predefinido de caracteres. Si el carácter no pertenece al conjunto, devuelve un valor desconocido (`-1`).

- **`key_2_matrix(s: str, md5_flag: bool = True) -> Matrix`:**
    Convierte una cadena en una matriz de enteros basada en su hash MD5 (4x4) o SHA-256 (8x8). Útil para generar claves de cifrado.

- **`text_2_matrix(data: str, md5_flag: bool = True, fill_value: str = '\0') -> Matrix`:**
    Convierte una cadena de texto en una matriz numérica de tamaño `Nx4` (MD5) o `Nx8` (SHA-256). Si la longitud de la cadena no es múltiplo del número de columnas, se rellena con un valor especificado (`\0` por defecto).

#### Uso:
Estas funciones son útiles para aplicaciones que requieren manipulación de texto y matrices, como cifrado, análisis de datos o procesamiento numérico.
