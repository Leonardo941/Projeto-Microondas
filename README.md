# **Simulador de Micro-ondas Digital**

## 📌 **Descrição**
Este projeto é uma simulação de um **micro-ondas digital**, desenvolvido utilizando **.NET** para o backend, seguindo os princípios de **Programação Orientada a Objetos (POO), SOLID, design patterns** e **boas práticas**. O frontend é implementado utilizando **HTML, CSS e JavaScript**, proporcionando uma interface intuitiva para o usuário.

---

## 🚀 **Funcionalidades**

### 🔹 **Nível 1**
- ✅ **Interface para entrada de tempo e potência** via teclado digital e/ou input de teclado.
- ✅ **Botão de "Início Rápido"** para aquecimento padrão (**30s, potência 10**).
- ✅ **Validações** para tempo (**1s a 2min**) e potência (**1 a 10, padrão 10**).
- ✅ **Conversão de tempo** acima de **60s** para formato **mm:ss**.
- ✅ **Possibilidade de adicionar 30s** durante o aquecimento.
- ✅ **Indicação visual do aquecimento** com strings variáveis conforme a potência.
- ✅ **Botão único para pausar e cancelar aquecimento**.

### 🔹 **Nível 2**
- ✅ **Programas de aquecimento pré-definidos** para alimentos específicos.
- ✅ **Instruções específicas** para cada programa.
- ✅ **Caracteres de aquecimento personalizados** para cada programa.
- ✅ **Impedimento de alteração ou exclusão** dos programas pré-definidos.

### 🔹 **Nível 3**
- ✅ **Cadastro de programas de aquecimento customizados**.
- ✅ **Validação para caracteres de aquecimento exclusivos**.
- ✅ **Diferenciação visual para programas customizados** (*exibidos em itálico*).
- ✅ **Persistência dos programas customizados via JSON ou SQL Server**.

---

## 🛠 **Tecnologias Utilizadas**

- **Backend:** .NET (C#)
- **Frontend:** HTML, CSS, JavaScript
- **Banco de Dados:** SQL Server (para persistência de programas customizados)
- **Arquitetura:** API REST, separação de camadas, boas práticas de POO e SOLID

---
