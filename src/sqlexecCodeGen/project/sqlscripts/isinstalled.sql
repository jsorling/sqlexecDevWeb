select cast(case when schema_id('sqlexecprojectstore') is null then 0 else 1 end as bit) installed