from fractions import Fraction
from copy import deepcopy
from typing import Self

_Matrix = list[list[int | Fraction]]
_List = list[int | Fraction]


def _simplify_type(value: int | float | Fraction) -> int | Fraction:
    if isinstance(value, (float, Fraction)):
        if value.is_integer():
            return int(value)
        else:
            return Fraction(value)
    elif isinstance(value, int):
        return value

    else:
        raise TypeError()


class Matrix:
    __Matrix: _Matrix

    def __init__(self, m: _Matrix): self.Matrix = m

    @property
    def Matrix(self): return self.__Matrix

    @Matrix.setter
    def Matrix(self, value):
        if not isinstance(value, list) or not all(isinstance(row, list) for row in value):
            raise TypeError("Matrix debe ser una lista de listas")

        n_rows = len(value)
        if n_rows == 0:
            raise ValueError("El numero de filas no puede ser 0")

        value = deepcopy(value)

        for ri in range(n_rows):
            n_cols = len(value[ri])
            if n_cols == 0:
                raise ValueError("El numero de columnas no puede ser 0")

            if ri > 0 and n_cols != len(value[ri - 1]):
                raise ValueError("El numero de columnas no es constante")

            for ci in range(n_cols):
                value[ri][ci] = _simplify_type(value[ri][ci])

        self.__Matrix = value

    @property
    def is_square(self): return self.n_rows == self.n_cols

    @property
    def n_rows(self): return len(self.__Matrix)

    @property
    def n_cols(self): return len(self.__Matrix[0])

    @property
    def det(self):
        if not self.is_square:
            raise ValueError("La matriz no es cuadrada")
        return Matrix._expansion_cofactors(self) if self.n_rows > 3 else Matrix._sarrus(self)

    @property
    def T(self):
        return Matrix([self.get_col(i) for i in range(self.n_cols)])

    @property
    def adj(self):
        m: _Matrix = []
        for ri in range(self.n_rows):
            m.append([])
            for ci in range(self.n_cols):
                det = self.divide_matrix(
                    exclude_row=[ri], exclude_col=[ci]).det
                m[ri].append((-1)**(ri + ci) * det)

        return Matrix(m).T

    @property
    def inv(self):
        return self.adj / self.det

    @property
    def flat(self):
        out = []
        for i in range(self.n_rows):
            for j in range(self.n_cols):
                out.append(self.get_item((i, j)))
        return out

    def get_item(self, indexes: tuple[int, int]) -> int | Fraction:
        i, j = indexes
        return self.__Matrix[i][j]

    def set_item(self, indexes: tuple[int, int], value: int | float | Fraction):
        i, j = indexes
        self.__Matrix[i][j] = _simplify_type(value)

    def get_row(self, index: int):
        return self.__Matrix[index]

    def get_col(self, index: int):
        return [r[index] for r in self.Matrix]

    def set_row(self, index: int, value: list[int | Fraction]):
        if len(value) == self.n_cols:
            for ci in range(self.n_cols):
                self.set_item((index, ci), value[ci])

        else:
            raise ValueError()

    def set_col(self, index: int, value):
        if len(value) == self.n_rows:
            for ri in range(self.n_rows):
                self.set_item((ri, index), value[ri])
        else:
            raise ValueError()

    def scale_item(self, indexes: tuple[int, int], scalar: int | float | Fraction) -> int | Fraction:
        item = self.get_item(indexes)
        scalar: int | Fraction = _simplify_type(scalar)

        return _simplify_type(item * scalar)

    def scale_row(self, index: int, scalar: int | float | Fraction) -> tuple[_List, int | Fraction]:
        return [self.scale_item((index, ic), scalar) for ic in range(self.n_cols)]

    def scale_col(self, index: int, scalar: int | float | Fraction) -> _List:
        return [self.scale_item((ir, index), scalar) for ir in range(self.n_rows)]

    def _scale_matrix(self, scalar: int | float | Fraction):
        return Matrix([self.scale_row(ri, scalar) for ri in range(self.n_rows)])

    def divide_matrix(
        self,
        start: tuple[int, int] | None = None,
        end: tuple[int, int] | None = None,
        exclude_row: list[int] | None = None,
        exclude_col: list[int] | None = None
    ):
        si, sj = start if start else (0, 0)
        ei, ej = end if end else (self.n_rows, self.n_cols)

        exclude_row = exclude_row if exclude_row else []
        exclude_col = exclude_col if exclude_col else []

        ei = ei if ei < self.n_rows else self.n_rows - 1
        ej = ej if ej < self.n_cols else self.n_cols - 1

        if si > ei or sj > ej:
            raise ValueError("Indices incorrectos.")

        m = []
        for i in range(si, ei + 1):
            if i in exclude_row:
                continue
            m.append([])  # Agrega una nueva fila vacía
            for j in range(sj, ej + 1):
                if j in exclude_col:
                    continue
                # Agregar a la última fila creada
                m[-1].append(self.get_item((i, j)))

        return Matrix(m)

    def dot(self, other: Self):
        m, n = self.n_rows, self.n_cols
        # q es el número de columnas de la segunda matriz
        p, q = other.n_rows, other.n_cols

        if n != p:
            raise ValueError("Tamaño de matrices inválidos")

        res = [[sum(self.get_item((i, k)) * other.get_item((k, j))
                # Usar q en lugar de p
                    for k in range(n)) for j in range(q)] for i in range(m)]

        return Matrix(res)

    @staticmethod
    def _expansion_cofactors(m: Matrix) -> int | Fraction:
        if not isinstance(m, Matrix):
            raise TypeError("El argumento debe ser de tipo Matrix.")

        det = 0
        sign = 1
        for fa in range(m.n_cols):
            factor = m.get_item((0, fa)) * sign
            sign *= -1
            new_m = m.divide_matrix(
                end=(m.n_rows - 1, m.n_cols - 1),
                exclude_row=[0],
                exclude_col=[fa])

            if new_m.n_rows < 4:
                det += factor * Matrix._sarrus(new_m)
            else:
                det += factor * Matrix._expansion_cofactors(new_m)

        return _simplify_type(det)

    @staticmethod
    def _sarrus(m: Matrix) -> int | Fraction:
        if not isinstance(m, Matrix):
            raise TypeError("El argumento debe ser de tipo Matrix.")

        if m.n_rows not in [1, 2, 3]:
            raise ValueError("La matriz debe ser de 3x3 o menor.")

        if m.n_rows == 1:
            return m.get_item((0, 0))

        elif m.n_rows == 2:
            aux1 = m.get_item((0, 0)) * m.get_item((1, 1))
            aux2 = m.get_item((0, 1)) * m.get_item((1, 0))

        else:
            aux1 = sum([
                m.get_item((0, 0)) * m.get_item((1, 1)) * m.get_item((2, 2)),
                m.get_item((0, 1)) * m.get_item((1, 2)) * m.get_item((2, 0)),
                m.get_item((0, 2)) * m.get_item((1, 0)) * m.get_item((2, 1))
            ])
            # Lado derecho
            aux2 = sum([
                m.get_item((0, 2)) * m.get_item((1, 1)) * m.get_item((2, 0)),
                m.get_item((0, 0)) * m.get_item((1, 2)) * m.get_item((2, 1)),
                m.get_item((0, 1)) * m.get_item((1, 0)) * m.get_item((2, 2))
            ])

        return _simplify_type(aux1 - aux2)

    def __mul__(self, value: int | float | Fraction | Self) -> int | Fraction | Self:
        if isinstance(value, (int, float, Fraction)):
            return self._scale_matrix(value)

        elif isinstance(value, Matrix):
            return self.dot(value)

        else:
            raise ValueError("El valor no es un escalar o una matriz")

    def __truediv__(self, scalar: int | float | Fraction) -> int | Fraction:
        return self._scale_matrix(Fraction(1, scalar))

    def __copy__(self):
        return Matrix(deepcopy(self.__Matrix))

    def __repr__(self):
        # ╭╮╯╰
        # ┌┐┘└
        out = ""
        for ri in range(self.n_rows):
            out += "│"
            row = self.get_row(ri)
            for ci in range(self.n_cols):
                ritem_str = str(row[ci])
                spaces = " " * (max(len(str(n))
                                for n in self.get_col(ci)) - len(ritem_str))
                out += spaces + ritem_str
                out += ", " if ci < self.n_cols - 1 else ""

            out += "│\n"

        row_len = len(out.split("\n")[0]) - 2
        aux1 = ("╭" + " " * row_len + "╮\n")
        out = aux1 + out

        aux2 = "╰" + " " * row_len + "╯\n"
        out += aux2

        return out
