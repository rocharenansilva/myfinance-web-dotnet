CREATE DATABASE myfinance

use myfinance

CREATE TABLE planoconta(
    id int IDENTITY(1, 1) NOT NULL,
    descricao varchar(50) NOT NULL,
    tipo char(1) NOT NULL,
    PRIMARY KEY (id)
);

CREATE TABLE transacao(
    id int IDENTITY(1, 1) NOT NULL,
    data DATETIME NOT NULL,
    valor DECIMAL(9, 2) NOT NULL,
    historico text,
    planocontaid int NOT NULL,
    PRIMARY KEY(id),
    FOREIGN KEY (planocontaid) REFERENCES planoconta(id)
);

INSERT INTO planoconta(descricao, tipo) VALUES ('Combustível', 'D')
INSERT INTO planoconta(descricao, tipo) VALUES ('Alimentação', 'D')
INSERT INTO planoconta(descricao, tipo) VALUES ('Plano de Saúde', 'D')
INSERT INTO planoconta(descricao, tipo) VALUES ('IPTU', 'D')
INSERT INTO planoconta(descricao, tipo) VALUES ('Salário', 'R')
INSERT INTO planoconta(descricao, tipo) VALUES ('Dividendos', 'R')
INSERT INTO planoconta(descricao, tipo) VALUES ('Bônus', 'R')

SELECT * FROM planoconta

INSERT INTO transacao(data, valor, historico, planocontaid) VALUES ('2023-01-10 07:47 AM', 100.80, 'Auto Posto Ipiranga', 1)
INSERT INTO transacao(data, valor, historico, planocontaid) VALUES ('2023-01-16 08:01 AM', 200.34, 'Auto Posto Ipiranga', 1)
INSERT INTO transacao(data, valor, historico, planocontaid) VALUES ('2023-01-20 01:23 PM', 64.50, 'Mc Donalds', 2)
INSERT INTO transacao(data, valor, historico, planocontaid) VALUES ('2023-01-15 09:00 AM', 786.30, 'Sul America Saúde', 3)
INSERT INTO transacao(data, valor, historico, planocontaid) VALUES ('2023-01-05 03:34 PM', 654.87, 'IPTU', 4)
INSERT INTO transacao(data, valor, historico, planocontaid) VALUES ('2023-01-05 01:23 PM', 59272.87, 'Salário', 5)
INSERT INTO transacao(data, valor, historico, planocontaid) VALUES ('2023-02-25 10:25 PM', 15847.90, 'Dividendos Google LLC', 6)
INSERT INTO transacao(data, valor, historico, planocontaid) VALUES ('2023-03-05 01:23 PM', 5452.98, 'Bônus Mensal', 7)

SELECT * FROM transacao

-- Todas as transações de Despesas no mês de Janeiro
SELECT t.valor, p.descricao
FROM transacao t
INNER JOIN planoconta p on t.planocontaid = p.id
WHERE p.tipo = 'D' and t.data >= '2023-01-01 00:00:00' and t.data <= '2023-01-31 23:59:00'

-- Todas as transações maiores que R$200
SELECT * FROM transacao WHERE valor > 200

-- Somatório de transações de receitas e despesas de todo o período
SELECT SUM(valor) as total_despesas FROM transacao t JOIN planoconta p on p.id = t.planocontaid
WHERE p.tipo = 'D'

SELECT despesas.total_despesas, receitas.total_receitas FROM
(SELECT SUM(valor) as total_despesas FROM transacao t JOIN planoconta p on p.id = t.planocontaid
WHERE p.tipo = 'D') as despesas,
(SELECT SUM(valor) as total_receitas FROM transacao t JOIN planoconta p on p.id = t.planocontaid
WHERE p.tipo = 'R') as receitas

-- Média de transações por mês
SELECT
AVG(valor) as media,
MONTH(data) as mes
FROM transacao
GROUP BY MONTH(data)

-- Transações e seus itens de plano de contas
SELECT t.id, t.data, t.valor, p.descricao, p.tipo
FROM transacao t 
JOIN planoconta p
ON t.planocontaid = p.id